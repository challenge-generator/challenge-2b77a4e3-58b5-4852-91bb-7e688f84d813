using Domain.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        services.AddOptions<AwsCodeWhispererOptions>()
            .Bind(configuration.GetSection("AWS:CodeWhisperer"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<AwsCodeWhispererOptions>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AwsCodeWhispererOptions>>();
            return options.Value;
        });

        services.AddSingleton<ICodeSuggestionService, AwsCodeWhispererService>();
        services.AddSingleton<ICodeCorrectionService, AwsCodeWhispererService>();
        services.AddSingleton<IDocumentationGeneratorService, AwsCodeWhispererService>();

        services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddConsole();
            builder.AddDebug();
        });

        services.AddHttpClient("CodeWhispererApi", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        RegisterAdditionalServices(services, configuration);

        return services;
    }

    private static void RegisterAdditionalServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<ICodeAnalysisCache, InMemoryCodeAnalysisCache>();

        var retrySettings = configuration.GetSection("AWS:RetrySettings").Get<RetrySettings>();
        if (retrySettings != null)
        {
            services.AddSingleton(retrySettings);
        }
        else
        {
            services.AddSingleton(new RetrySettings
            {
                MaxRetries = 3,
                DelayMilliseconds = 1000,
                ExponentialBackoff = true
            });
        }
    }

    public static IServiceCollection AddCodeWhispererWithResilience(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);

        services.AddSingleton<ICircuitBreakerService, CircuitBreakerService>();

        services.AddSingleton<IRetryPolicyProvider, DefaultRetryPolicyProvider>();

        return services;
    }

    public static IServiceCollection AddHealthChecksForInfrastructure(
        this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("aws-credentials", () =>
            {
                try
                {
                    return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(
                        "AWS credentials configured");
                }
                catch
                {
                    return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Unhealthy(
                        "AWS credentials not configured");
                }
            });

        return services;
    }
}

public interface ICodeAnalysisCache
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default);
    Task InvalidateAsync(string key, CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken = default);
}

public class InMemoryCodeAnalysisCache : ICodeAnalysisCache
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly SemaphoreSlim _lock = new(1, 1);

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (entry.Expiration > DateTime.UtcNow)
                {
                    return (T?)entry.Value;
                }
                _cache.Remove(key);
            }
            return default;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task SetAsync<T>(
        string key, 
        T value, 
        TimeSpan expiration, 
        CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _cache[key] = new CacheEntry
            {
                Value = value,
                Expiration = DateTime.UtcNow.Add(expiration)
            };
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task InvalidateAsync(string key, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _cache.Remove(key);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _cache.Clear();
        }
        finally
        {
            _lock.Release();
        }
    }

    private class CacheEntry
    {
        public object? Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}

public class RetrySettings
{
    public int MaxRetries { get; set; } = 3;
    public int DelayMilliseconds { get; set; } = 1000;
    public bool ExponentialBackoff { get; set; } = true;
    public int MaxDelayMilliseconds { get; set; } = 30000;
}

public interface ICircuitBreakerService
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
    Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
    CircuitBreakerState GetState();
    void Reset();
}

public enum CircuitBreakerState
{
    Closed,
    Open,
    HalfOpen
}

public class CircuitBreakerService : ICircuitBreakerService
{
    private readonly ILogger<CircuitBreakerService> _logger;
    private readonly object _lock = new();
    private CircuitBreakerState _state = CircuitBreakerState.Closed;
    private int _failureCount = 0;
    private int _successCount = 0;
    private DateTime _lastFailureTime = DateTime.MinValue;
    private const int FailureThreshold = 5;
    private const int SuccessThreshold = 2;
    private static readonly TimeSpan OpenStateDuration = TimeSpan.FromSeconds(30);

    public CircuitBreakerService(ILogger<CircuitBreakerService> logger)
    {
        _logger = logger;
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        if (_state == CircuitBreakerState.Open)
        {
            if (DateTime.UtcNow - _lastFailureTime > OpenStateDuration)
            {
                _state = CircuitBreakerState.HalfOpen;
                _logger.LogInformation("CircuitBreaker transitioned to HalfOpen state");
            }
            else
            {
                throw new CircuitBreakerOpenException("Circuit breaker is open");
            }
        }

        try
        {
            var result = await action();
            OnSuccess();
            return result;
        }
        catch (Exception ex)
        {
            OnFailure();
            _logger.LogError(ex, "CircuitBreaker execution failed");
            throw;
        }
    }

    public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(async () =>
        {
            await action();
            return true;
        }, cancellationToken);
    }

    private void OnSuccess()
    {
        lock (_lock)
        {
            _failureCount = 0;
            _successCount++;

            if (_state == CircuitBreakerState.HalfOpen && _successCount >= SuccessThreshold)
            {
                _state = CircuitBreakerState.Closed;
                _logger.LogInformation("CircuitBreaker transitioned to Closed state");
            }
        }
    }

    private void OnFailure()
    {
        lock (_lock)
        {
            _failureCount++;
            _successCount = 0;
            _lastFailureTime = DateTime.UtcNow;

            if (_failureCount >= FailureThreshold)
            {
                _state = CircuitBreakerState.Open;
                _logger.LogWarning("CircuitBreaker transitioned to Open state after {Count} failures", FailureThreshold);
            }
        }
    }

    public CircuitBreakerState GetState() => _state;

    public void Reset()
    {
        lock (_lock)
        {
            _state = CircuitBreakerState.Closed;
            _failureCount = 0;
            _successCount = 0;
            _lastFailureTime = DateTime.MinValue;
        }
    }
}

public class CircuitBreakerOpenException : Exception
{
    public CircuitBreakerOpenException(string message) : base(message) { }
}

public interface IRetryPolicyProvider
{
    IRetryPolicy GetRetryPolicy();
}

public interface IRetryPolicy
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
}

public class DefaultRetryPolicyProvider : IRetryPolicyProvider
{
    private readonly RetrySettings _settings;
    private readonly ILogger<DefaultRetryPolicyProvider> _logger;

    public DefaultRetryPolicyProvider(
        IOptions<RetrySettings> settings,
        ILogger<DefaultRetryPolicyProvider> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public IRetryPolicy GetRetryPolicy()
    {
        return new ExponentialBackoffRetryPolicy(_settings, _logger);
    }
}

public class ExponentialBackoffRetryPolicy : IRetryPolicy
{
    private readonly RetrySettings _settings;
    private readonly ILogger _logger;

    public ExponentialBackoffRetryPolicy(RetrySettings settings, ILogger logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        var attempt = 0;
        Exception? lastException = null;

        while (attempt < _settings.MaxRetries)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                attempt++;
                lastException = ex;
                
                if (attempt >= _settings.MaxRetries)
                {
                    _logger.LogError(ex, "All {MaxRetries} retry attempts failed", _settings.MaxRetries);
                    break;
                }

                var delay = _settings.ExponentialBackoff
                    ? Math.Min(
                        _settings.DelayMilliseconds * (int)Math.Pow(2, attempt - 1),
                        _settings.MaxDelayMilliseconds)
                    : _settings.DelayMilliseconds;

                _logger.LogWarning("Retry attempt {Attempt} failed. Waiting {Delay}ms before next attempt",
                    attempt, delay);

                await Task.Delay(delay, cancellationToken);
            }
        }

        throw new RetryExhaustedException(
            $"Retry policy exhausted after {_settings.MaxRetries} attempts",
            lastException);
    }
}

public class RetryExhaustedException : Exception
{
    public RetryExhaustedException(string message, Exception? innerException)
        : base(message, innerException) { }
}