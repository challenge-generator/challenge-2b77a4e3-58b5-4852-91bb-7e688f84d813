using Amazon.CodeWhisperer;
using Amazon.CodeWhisperer.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Services;

public class AwsCodeWhispererService : ICodeSuggestionService, ICodeCorrectionService, IDocumentationGeneratorService
{
    private readonly AmazonCodeWhispererClient _whispererClient;
    private readonly ILogger<AwsCodeWhispererService> _logger;
    private readonly AwsCodeWhispererOptions _options;
    private readonly HttpClient _httpClient;

    public AwsCodeWhispererService(
        IOptions<AwsCodeWhispererOptions> options,
        ILogger<AwsCodeWhispererService> logger)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        var credentials = GetCredentials();
        var config = new AmazonCodeWhispererConfig
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_options.Region),
            Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds)
        };
        
        _whispererClient = new AmazonCodeWhispererClient(credentials, config);
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
    }

    private AWSCredentials GetCredentials()
    {
        var chain = new CredentialProfileStoreChain();
        if (chain.TryGetAWSCredentials(_options.ProfileName, out var credentials))
        {
            return credentials!;
        }
        
        if (!string.IsNullOrEmpty(_options.AccessKeyId) && !string.IsNullOrEmpty(_options.SecretAccessKey))
        {
            return new BasicAWSCredentials(_options.AccessKeyId, _options.SecretAccessKey);
        }
        
        return new AnonymousAWSCredentials();
    }

    public async Task<CodeSuggestionResponse> GenerateSuggestionsAsync(
        CodeSuggestionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Generando sugerencias de código para contexto: {Context}", 
            request.ContextDescription);

        try
        {
            var suggestions = new List<CodeSuggestion>();
            var startTime = DateTime.UtcNow;

            var generateSuggestionsRequest = new GenerateSuggestionsRequest
            {
                FileContext = request.CurrentCode,
                MaxSuggestions = request.SuggestionsCount,
                CompletionType = CompletionType.Auto,
                FeatureContext = request.ContextDescription
            };

            var response = await _whispererClient.GenerateSuggestionsAsync(generateSuggestionsRequest, cancellationToken);

            if (response.Suggestions != null && response.Suggestions.Any())
            {
                var index = 0;
                foreach (var suggestion in response.Suggestions)
                {
                    suggestions.Add(new CodeSuggestion(
                        suggestion.Content,
                        suggestion.Confidence ?? 0.85,
                        GetSuggestionType(suggestion.SuggestionType),
                        $"Suggestion-{Guid.NewGuid():N}"
                    ));
                    index++;
                }
            }
            else
            {
                suggestions.AddRange(GenerateFallbackSuggestions(request));
            }

            var duration = DateTime.UtcNow - startTime;
            _logger.LogInformation("Generadas {Count} sugerencias en {Duration}ms", 
                suggestions.Count, duration.TotalMilliseconds);

            return new CodeSuggestionResponse(suggestions, request.ContextDescription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar sugerencias de código");
            return new CodeSuggestionResponse(GenerateFallbackSuggestions(request), request.ContextDescription);
        }
    }

    private IEnumerable<CodeSuggestion> GenerateFallbackSuggestions(CodeSuggestionRequest request)
    {
        var suggestions = new List<CodeSuggestion>();
        var codeLines = request.CurrentCode.Split('\n');
        var lastLine = codeLines.Length > 0 ? codeLines.Last().Trim() : string.Empty;

        if (!lastLine.Contains("{") && !lastLine.Contains("}"))
        {
            suggestions.Add(new CodeSuggestion(
                "// Considera agregar documentación XML para el método",
                0.75,
                "documentation",
                $"Fallback-{Guid.NewGuid():N}"
            ));
        }

        if (request.CurrentCode.Contains("foreach") && !request.CurrentCode.Contains("null"))
        {
            suggestions.Add(new CodeSuggestion(
                "// Considera verificar null antes de iterar",
                0.80,
                "best-practice",
                $"Fallback-{Guid.NewGuid():N}"
            ));
        }

        return suggestions;
    }

    private static string GetSuggestionType(string? awsType)
    {
        return awsType?.ToLowerInvariant() switch
        {
            "completion" => "code-completion",
            "refactor" => "refactoring",
            "comment" => "documentation",
            _ => "general"
        };
    }

    public IDictionary<CodeSuggestion, bool> ValidateSuggestions(IEnumerable<CodeSuggestion> suggestions)
    {
        var results = new Dictionary<CodeSuggestion, bool>();
        
        foreach (var suggestion in suggestions)
        {
            var isValid = !string.IsNullOrWhiteSpace(suggestion.SuggestedCode) &&
                         suggestion.SuggestedCode.Length <= 10000 &&
                         suggestion.ConfidenceScore >= 0 &&
                         suggestion.ConfidenceScore <= 1;
            
            results[suggestion] = isValid;
        }
        
        _logger.LogInformation("Validadas {Count} sugerencias, {Valid} válidas",
            results.Count, results.Count(v => v.Value));
        
        return results;
    }

    public async Task<IEnumerable<SuggestionMetrics>> GetPerformanceMetricsAsync(
        CancellationToken cancellationToken = default)
    {
        var metrics = new List<SuggestionMetrics>
        {
            new SuggestionMetrics
            {
                TotalSuggestions = 0,
                AcceptedSuggestions = 0,
                RejectedSuggestions = 0,
                AverageConfidence = 0.0,
                AverageResponseTimeMs = 0,
                Timestamp = DateTime.UtcNow
            }
        };

        return await Task.FromResult(metrics);
    }

    public async Task<CodeCorrectionResponse> CorrectCodeAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Corrigiendo código con severidad: {Severity}", request.SeverityLevel);

        try
        {
            var corrections = new List<string>();
            var correctedCode = request.ProblematicCode;
            var explanation = string.Empty;
            double confidenceScore = 0.9;

            correctedCode = ApplyBasicCorrections(correctedCode, request.ErrorDescription, corrections);
            
            if (request.SeverityLevel >= 1)
            {
                correctedCode = ApplySecurityCorrections(correctedCode, corrections);
            }
            
            if (request.SeverityLevel >= 2)
            {
                correctedCode = ApplyPerformanceCorrections(correctedCode, corrections);
                confidenceScore = 0.75;
            }

            explanation = $"Se aplicaron {corrections.Count} correcciones al código. " +
                         $"El código corregido aborda los problemas identificados con nivel de severidad {request.SeverityLevel}.";

            _logger.LogInformation("Corrección completada: {Count} cambios aplicados", corrections.Count);

            return new CodeCorrectionResponse(correctedCode, explanation, corrections, confidenceScore);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al corregir código");
            throw new CodeCorrectionException("Error al procesar la corrección de código", ex);
        }
    }

    private string ApplyBasicCorrections(string code, string errorDescription, List<string> corrections)
    {
        var corrected = code;

        if (errorDescription.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            corrected = corrected.Replace("object", "object?");
            corrections.Add("Agregado null-forgiving operator para tipos potencialmente nulos");
        }

        if (errorDescription.Contains("unused", StringComparison.OrdinalIgnoreCase))
        {
            corrections.Add("Identificada variable no utilizada - considerar eliminar");
        }

        if (!corrected.Contains("namespace") && corrected.Contains("class"))
        {
            corrections.Add("Código fuera de namespace - se recomienda envolver en namespace");
        }

        return corrected;
    }

    private string ApplySecurityCorrections(string code, List<string> corrections)
    {
        var corrected = code;

        if (code.Contains("password") || code.Contains("secret") || code.Contains("apiKey"))
        {
            corrected = corrected.Replace("=\"", "=Environment.GetEnvironmentVariable(\"");
            corrections.Add("Credenciales movidas a variables de entorno");
        }

        if (code.Contains("SELECT *"))
        {
            corrected = corrected.Replace("SELECT *", "SELECT columnas_especificas");
            corrections.Add("Evitado SELECT * para prevenir exposición de datos");
        }

        return corrected;
    }

    private string ApplyPerformanceCorrections(string code, List<string> corrections)
    {
        var corrected = code;

        if (code.Contains("foreach") && !code.Contains("ToList"))
        {
            corrections.Add("Considerar usar lazy enumeration en lugar de evaluación eager");
        }

        if (code.Contains("new HttpClient()"))
        {
            corrections.Add("HttpClient debe ser inyectado como singleton para evitar agotamiento de sockets");
        }

        return corrected;
    }

    public async Task<IEnumerable<CodeIssue>> AnalyzeCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        var issues = new List<CodeIssue>();

        if (string.IsNullOrWhiteSpace(code))
        {
            return issues;
        }

        if (code.Contains("SELECT *"))
        {
            issues.Add(new CodeIssue
            {
                LineNumber = code.Split('\n').ToList().FindIndex(l => l.Contains("SELECT *")) + 1,
                Severity = 1,
                Description = "Uso de SELECT * puede causar problemas de rendimiento y exposición de datos",
                CodeSnippet = "SELECT *"
            });
        }

        if (code.Contains("new HttpClient()"))
        {
            issues.Add(new CodeIssue
            {
                LineNumber = code.Split('\n').ToList().FindIndex(l => l.Contains("new HttpClient()")) + 1,
                Severity = 2,
                Description = "Instanciación de HttpClient en método causa agotamiento de recursos",
                CodeSnippet = "new HttpClient()"
            });
        }

        if (code.Contains("catch (Exception)") || code.Contains("catch(Exception)"))
        {
            issues.Add(new CodeIssue
            {
                LineNumber = code.Split('\n').ToList().FindIndex(l => l.Contains("catch")) + 1,
                Severity = 1,
                Description = "Captura de Exception genérica oculta errores específicos",
                CodeSnippet = "catch (Exception)"
            });
        }

        return await Task.FromResult(issues);
    }

    public async Task<int> EstimateCorrectionComplexityAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default)
    {
        var complexity = 1;

        if (request.ProblematicCode.Contains("async") && !request.ProblematicCode.Contains("await"))
        {
            complexity += 2;
        }

        if (request.ProblematicCode.Length > 500)
        {
            complexity += 1;
        }

        complexity += request.SeverityLevel * 2;

        return await Task.FromResult(complexity);
    }

    public async Task<DocumentationResponse> GenerateDocumentationAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Generando documentación para snippet de {Length} caracteres", 
            request.CodeSnippet.Length);

        try
        {
            var documentation = new List<DocumentationSection>();
            var codeLines = request.CodeSnippet.Split('\n');
            var currentSection = new DocumentationSection
            {
                Title = "Resumen",
                Content = GenerateSummaryFromCode(codeLines),
                Order = 1
            };
            documentation.Add(currentSection);

            if (codeLines.Any(l => l.Contains("///") || l.Contains("/**")))
            {
                var existingDocs = ExtractExistingDocumentation(codeLines);
                documentation.Add(new DocumentationSection
                {
                    Title = "Documentación Existente",
                    Content = existingDocs,
                    Order = 2
                });
            }

            var paramDocs = ExtractParameterDocumentation(codeLines);
            if (!string.IsNullOrEmpty(paramDocs))
            {
                documentation.Add(new DocumentationSection
                {
                    Title = "Parámetros",
                    Content = paramDocs,
                    Order = 3
                });
            }

            var returnDoc = ExtractReturnDocumentation(codeLines);
            if (!string.IsNullOrEmpty(returnDoc))
            {
                documentation.Add(new DocumentationSection
                {
                    Title = "Retorno",
                    Content = returnDoc,
                    Order = 4
                });
            }

            _logger.LogInformation("Generada documentación con {Count} secciones", documentation.Count);

            return await Task.FromResult(new DocumentationResponse(
                documentation,
                request.CodeSnippet,
                true
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar documentación");
            throw new DocumentationGenerationException("Error al generar documentación", ex);
        }
    }

    private string GenerateSummaryFromCode(string[] codeLines)
    {
        var classOrMethod = codeLines.FirstOrDefault(l => 
            l.Contains("class ") || 
            l.Contains("interface ") || 
            l.Contains("method ") ||
            l.Contains("public ") ||
            l.Contains("private ") ||
            l.Contains("protected "));

        if (string.IsNullOrEmpty(classOrMethod))
        {
            return "Código sin clasificación clara. Se recomienda agregar documentación.";
        }

        if (classOrMethod.Contains("class"))
        {
            var className = ExtractName(classOrMethod, "class");
            return $"Representa la clase {className} que encapsula la lógica de negocio relacionada.";
        }

        if (classOrMethod.Contains("interface"))
        {
            var interfaceName = ExtractName(classOrMethod, "interface");
            return $"Definición del contrato {interfaceName} para la interacción con componentes externos.";
        }

        return "Método que ejecuta una operación específica dentro del contexto de la aplicación.";
    }

    private string ExtractName(string line, string keyword)
    {
        var parts = line.Split(keyword);
        if (parts.Length > 1)
        {
            var namePart = parts[1].Trim().Split(' ')[0].Split('{')[0].Trim();
            return namePart;
        }
        return "Desconocido";
    }

    private string ExtractExistingDocumentation(string[] codeLines)
    {
        var docLines = codeLines
            .Where(l => l.Trim().StartsWith("///") || l.Trim().StartsWith("/**"))
            .Select(l => l.Replace("///", "").Replace("/**", "").Replace("*/", "").Trim());

        return string.Join("\n", docLines);
    }

    private string ExtractParameterDocumentation(string[] codeLines)
    {
        var paramLines = codeLines
            .Where(l => l.Contains("@param") || l.Contains("/// <param"))
            .Select(l => l.Replace("@param", "").Replace("/// <param name=", "").Replace("</param>", "").Trim());

        return string.Join("\n", paramLines);
    }

    private string ExtractReturnDocumentation(string[] codeLines)
    {
        var returnLine = codeLines
            .FirstOrDefault(l => l.Contains("@return") || l.Contains("/// <returns"));

        if (returnLine == null)
            return string.Empty;

        return returnLine.Replace("@return", "").Replace("/// <returns>", "").Replace("</returns>", "").Trim();
    }
}

public class AwsCodeWhispererOptions
{
    public string Region { get; set; } = "us-east-1";
    public string ProfileName { get; set; } = "default";
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public int TimeoutSeconds { get; set; } = 30;
    public bool EnableAutoSuggestions { get; set; } = true;
    public int MaxTokensPerSuggestion { get; set; } = 256;
}