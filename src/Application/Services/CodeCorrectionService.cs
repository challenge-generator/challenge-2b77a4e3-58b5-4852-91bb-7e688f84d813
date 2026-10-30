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

public class CodeCorrectionService : ICodeCorrectionService
{
    private readonly ILogger<CodeCorrectionService> _logger;
    private readonly ICodeCorrectionService _infrastructureService;
    private readonly Dictionary<string, List<CodeIssue>> _analysisCache = new();
    private readonly object _cacheLock = new();
    private const int MaxCacheSize = 500;
    private const double HighSeverityThreshold = 1.5;

    public CodeCorrectionService(
        ILogger<CodeCorrectionService> logger,
        ICodeCorrectionService infrastructureService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _infrastructureService = infrastructureService ?? throw new ArgumentNullException(nameof(infrastructureService));
    }

    public async Task<CodeCorrectionResponse> CorrectCodeAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.ProblematicCode))
            throw new CodeCorrectionException("El código problemático no puede estar vacío");

        _logger.LogInformation("Iniciando corrección de código con severidad: {SeverityLevel}",
            request.SeverityLevel);

        try
        {
            var startTime = DateTime.UtcNow;
            var response = await _infrastructureService.CorrectCodeAsync(request, cancellationToken);
            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation("Corrección completada en {Duration}ms. Éxito: {IsSuccess}",
                duration.TotalMilliseconds, response.IsSuccess);

            if (response.RequiresManualReview())
            {
                _logger.LogWarning("La corrección requiere revisión manual. Puntuación de confianza: {Confidence}",
                    response.ConfidenceScore);
            }

            return response;
        }
        catch (CodeCorrectionException ex)
        {
            _logger.LogError(ex, "Error al corregir el código");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante la corrección");
            throw new CodeCorrectionException("Error al procesar la solicitud de corrección", ex);
        }
    }

    public async Task<IEnumerable<CodeIssue>> AnalyzeCodeAsync(
        string code,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("El código a analizar no puede estar vacío", nameof(code));

        var cacheKey = GenerateCacheKey(code);

        lock (_cacheLock)
        {
            if (_analysisCache.TryGetValue(cacheKey, out var cachedIssues))
            {
                _logger.LogDebug("Usando análisis en caché para clave: {CacheKey}", cacheKey);
                return cachedIssues;
            }
        }

        _logger.LogDebug("Analizando código de longitud: {Length}", code.Length);

        try
        {
            var issues = await _infrastructureService.AnalyzeCodeAsync(code, cancellationToken);
            var issueList = issues.ToList();

            lock (_cacheLock)
            {
                _analysisCache[cacheKey] = issueList;

                if (_analysisCache.Count > MaxCacheSize)
                {
                    var keysToRemove = _analysisCache.Keys.Take(_analysisCache.Count - MaxCacheSize).ToList();
                    foreach (var key in keysToRemove)
                    {
                        _analysisCache.Remove(key);
                    }
                }
            }

            var criticalCount = issueList.Count(i => i.IsCritical());
            _logger.LogInformation("Análisis completado: {Total} problemas encontrados, {Critical} críticos",
                issueList.Count, criticalCount);

            return issueList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al analizar el código");
            throw new CodeCorrectionException("Error durante el análisis de código", ex);
        }
    }

    public async Task<int> EstimateCorrectionComplexityAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogDebug("Estimando complejidad de corrección para código de longitud: {Length}",
            request.ProblematicCode?.Length ?? 0);

        try
        {
            var complexity = await _infrastructureService.EstimateCorrectionComplexityAsync(request, cancellationToken);

            _logger.LogInformation("Complejidad estimada: {Complexity}", complexity);

            return complexity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al estimar la complejidad");
            return CalculateLocalComplexity(request);
        }
    }

    private string GenerateCacheKey(string code)
    {
        var hash = code.GetHashCode();
        var length = code.Length;
        return $"{length}_{hash}";
    }

    private int CalculateLocalComplexity(CodeCorrectionRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.ProblematicCode))
            return 0;

        var code = request.ProblematicCode;
        int complexity = 1;

        complexity += code.Count(c => c == '{') + code.Count(c => c == '}');
        complexity += code.Split('\n').Length / 10;
        complexity += request.SeverityLevel;

        if (code.Contains("async") || code.Contains("await"))
            complexity += 2;

        if (code.Contains("foreach") || code.Contains("for") || code.Contains("while"))
            complexity += code.Split(new[] { "foreach", "for", "while" }, StringSplitOptions.None).Length - 1;

        return Math.Min(complexity, 10);
    }

    public async Task<CodeCorrectionResponse> CorrectCodeWithFallbackAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await CorrectCodeAsync(request, cancellationToken);
        }
        catch (CodeCorrectionException ex)
        {
            _logger.LogWarning(ex, "Corrección primaria falló, generando respuesta de fallback");

            return new CodeCorrectionResponse(
                request.ProblematicCode,
                "No se pudo completar la corrección automática. Se recomienda revisión manual.",
                Enumerable.Empty<string>(),
                0.0
            );
        }
    }

    public async Task<IEnumerable<CodeIssue>> AnalyzeCodeWithDepthAsync(
        string code,
        int maxDepth = 3,
        CancellationToken cancellationToken = default)
    {
        if (maxDepth <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxDepth), "La profundidad debe ser mayor que 0");

        var allIssues = new List<CodeIssue>();

        for (int depth = 1; depth <= maxDepth; depth++)
        {
            _logger.LogDebug("Ejecutando análisis en nivel de profundidad: {Depth}", depth);
            var issues = await AnalyzeCodeAsync(code, cancellationToken);
            allIssues.AddRange(issues);
        }

        return allIssues.Distinct().ToList();
    }

    public void ClearCache()
    {
        lock (_cacheLock)
        {
            var count = _analysisCache.Count;
            _analysisCache.Clear();
            _logger.LogInformation("Caché de análisis limpiada. Elementos eliminados: {Count}", count);
        }
    }
}