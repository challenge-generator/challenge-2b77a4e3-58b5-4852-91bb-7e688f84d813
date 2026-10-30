using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CodeSuggestionService : ICodeSuggestionService
{
    private readonly ILogger<CodeSuggestionService> _logger;
    private readonly ICodeSuggestionService _infrastructureService;
    private readonly List<SuggestionMetrics> _metricsCache = new();
    private readonly object _metricsLock = new();
    private const int MaxSuggestionsPerRequest = 10;
    private const double MinConfidenceThreshold = 0.3;

    public CodeSuggestionService(
        ILogger<CodeSuggestionService> logger,
        ICodeSuggestionService infrastructureService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _infrastructureService = infrastructureService ?? throw new ArgumentNullException(nameof(infrastructureService));
    }

    public async Task<CodeSuggestionResponse> GenerateSuggestionsAsync(
        CodeSuggestionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.CurrentCode))
            throw new CodeSuggestionException("El código actual no puede estar vacío");

        _logger.LogInformation("Generando sugerencias de código para contexto: {ContextDescription}", 
            request.ContextDescription);

        try
        {
            var startTime = DateTime.UtcNow;
            var response = await _infrastructureService.GenerateSuggestionsAsync(request, cancellationToken);
            var duration = DateTime.UtcNow - startTime;

            RecordMetrics(request, response, duration);

            if (!response.HasValidSuggestions())
            {
                _logger.LogWarning("No se generaron sugerencias válidas para el código proporcionado");
            }
            else
            {
                _logger.LogInformation("Se generaron {Count} sugerencias en {Duration}ms",
                    response.Suggestions.Count(), duration.TotalMilliseconds);
            }

            return response;
        }
        catch (CodeSuggestionException ex)
        {
            _logger.LogError(ex, "Error al generar sugerencias de código");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al generar sugerencias");
            throw new CodeSuggestionException("Error al procesar la solicitud de sugerencias", ex);
        }
    }

    public IDictionary<CodeSuggestion, bool> ValidateSuggestions(IEnumerable<CodeSuggestion> suggestions)
    {
        if (suggestions == null)
            throw new ArgumentNullException(nameof(suggestions));

        var validationResults = new Dictionary<CodeSuggestion, bool>();
        var suggestionList = suggestions.ToList();

        _logger.LogDebug("Validando {Count} sugerencias de código", suggestionList.Count);

        foreach (var suggestion in suggestionList)
        {
            var isValid = ValidateSingleSuggestion(suggestion);
            validationResults[suggestion] = isValid;

            _logger.LogTrace("Sugerencia {Id} validada: {IsValid}", suggestion.Id, isValid);
        }

        var validCount = validationResults.Count(v => v.Value);
        _logger.LogInformation("Validación completada: {ValidCount}/{Total} sugerencias válidas",
            validCount, suggestionList.Count);

        return validationResults;
    }

    public async Task<IEnumerable<SuggestionMetrics>> GetPerformanceMetricsAsync(
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Obteniendo métricas de rendimiento de sugerencias");

        await Task.Delay(1, cancellationToken);

        lock (_metricsLock)
        {
            return _metricsCache.ToList().AsReadOnly();
        }
    }

    private bool ValidateSingleSuggestion(CodeSuggestion suggestion)
    {
        if (suggestion == null)
            return false;

        if (string.IsNullOrWhiteSpace(suggestion.SuggestedCode))
            return false;

        if (suggestion.SuggestedCode.Length > 10000)
            return false;

        if (suggestion.ConfidenceScore < MinConfidenceThreshold)
            return false;

        if (string.IsNullOrWhiteSpace(suggestion.Description))
            return false;

        return true;
    }

    private void RecordMetrics(CodeSuggestionRequest request, CodeSuggestionResponse response, TimeSpan duration)
    {
        var metrics = new SuggestionMetrics
        {
            RequestId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow,
            ContextDescription = request.ContextDescription,
            SuggestionsCount = response.Suggestions.Count(),
            DurationMilliseconds = duration.TotalMilliseconds,
            Success = response.HasValidSuggestions(),
            AverageConfidence = response.Suggestions.Any() 
                ? response.Suggestions.Average(s => s.ConfidenceScore) 
                : 0.0
        };

        lock (_metricsLock)
        {
            _metricsCache.Add(metrics);

            if (_metricsCache.Count > 1000)
            {
                _metricsCache.RemoveRange(0, _metricsCache.Count - 1000);
            }
        }

        _logger.LogDebug("Métricas registradas: RequestId={RequestId}, Duration={Duration}ms",
            metrics.RequestId, metrics.DurationMilliseconds);
    }

    public async Task<IEnumerable<CodeSuggestion>> FilterHighConfidenceSuggestionsAsync(
        IEnumerable<CodeSuggestion> suggestions,
        double minimumConfidence = 0.7,
        CancellationToken cancellationToken = default)
    {
        if (suggestions == null)
            return Enumerable.Empty<CodeSuggestion>();

        await Task.Delay(1, cancellationToken);

        return suggestions
            .Where(s => s.ConfidenceScore >= minimumConfidence)
            .OrderByDescending(s => s.ConfidenceScore)
            .ToList();
    }

    public async Task<CodeSuggestionResponse> GenerateSuggestionsWithRetryAsync(
        CodeSuggestionRequest request,
        int maxRetries = 3,
        CancellationToken cancellationToken = default)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                _logger.LogInformation("Intento {Attempt} de {MaxRetries} para generar sugerencias",
                    attempt, maxRetries);

                return await GenerateSuggestionsAsync(request, cancellationToken);
            }
            catch (CodeSuggestionException ex) when (attempt < maxRetries)
            {
                _logger.LogWarning(ex, "Reintentando después de error en intento {Attempt}", attempt);
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), cancellationToken);
            }
        }

        throw new CodeSuggestionException("No se pudieron generar sugerencias después de múltiples intentos");
    }
}