namespace Application.Services;

using Domain.Interfaces;
using Domain.Models;
using Application.DTOs;

public class DocumentationGeneratorService : IDocumentationGeneratorService
{
    private readonly ICodeSuggestionService _suggestionService;
    private readonly ICodeCorrectionService _correctionService;
    private readonly ILogger<DocumentationGeneratorService> _logger;
    private const int MaxDocumentationLength = 10000;
    private const int DefaultContextLines = 10;

    public DocumentationGeneratorService(
        ICodeSuggestionService suggestionService,
        ICodeCorrectionService correctionService,
        ILogger<DocumentationGeneratorService> logger)
    {
        _suggestionService = suggestionService ?? throw new ArgumentNullException(nameof(suggestionService));
        _correctionService = correctionService ?? throw new ArgumentNullException(nameof(correctionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DocumentationResponse> GenerateDocumentationAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new DocumentationGenerationException("La solicitud de documentación no puede ser nula.");
        }

        _logger.LogInformation("Iniciando generación de documentación para snippet de {CodeLength} caracteres",
            request.CodeSnippet?.Length ?? 0);

        try
        {
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validación de solicitud falló: {Errors}", string.Join(", ", validationResult.Errors));
                throw new DocumentationGenerationException($"Validación fallida: {string.Join(", ", validationResult.Errors)}");
            }

            var documentationContent = await GenerateDocumentationContentAsync(request, cancellationToken);
            
            var response = new DocumentationResponse
            {
                Documentation = documentationContent.GeneratedContent,
                SourceCode = request.CodeSnippet,
                Comment = request.Comment,
                ScopeLines = request.ScopeLines,
                ConfidenceScore = documentationContent.ConfidenceScore,
                GeneratedAt = DateTime.UtcNow,
                Metadata = new Dictionary<string, object>
                {
                    { "processingTimeMs", documentationContent.ProcessingTime.TotalMilliseconds },
                    { "sectionsGenerated", documentationContent.Sections.Count },
                    { "originalCodeLength", request.CodeSnippet.Length }
                }
            };

            _logger.LogInformation("Documentación generada exitosamente con confianza {Confidence}",
                response.ConfidenceScore);

            return response;
        }
        catch (DocumentationGenerationException)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Generación de documentación cancelada");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al generar documentación");
            throw new DocumentationGenerationException("Error al generar documentación", ex);
        }
    }

    public async Task<IEnumerable<DocumentationSection>> GetDocumentationSectionsAsync(
        string codeSnippet,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(codeSnippet))
        {
            return Enumerable.Empty<DocumentationSection>();
        }

        var sections = new List<DocumentationSection>();
        var lines = codeSnippet.Split('\n');
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (IsDocumentableLine(line))
            {
                sections.Add(new DocumentationSection
                {
                    LineNumber = i + 1,
                    Content = line,
                    SectionType = DetermineSectionType(line),
                    Description = await AnalyzeLineForDocumentationAsync(line, cancellationToken)
                });
            }
        }

        return sections;
    }

    public async Task<DocumentationMetrics> GetDocumentationMetricsAsync(
        DocumentationResponse response,
        CancellationToken cancellationToken = default)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        var metrics = new DocumentationMetrics
        {
            TotalCharacters = response.Documentation?.Length ?? 0,
            TotalWords = CountWords(response.Documentation ?? string.Empty),
            TotalLines = CountLines(response.Documentation ?? string.Empty),
            ConfidenceScore = response.ConfidenceScore,
            GeneratedAt = response.GeneratedAt,
            ProcessingTimeMs = response.Metadata?.ContainsKey("processingTimeMs") == true
                ? Convert.ToDouble(response.Metadata["processingTimeMs"])
                : 0
        };

        metrics.CompletenessScore = CalculateCompletenessScore(metrics);
        metrics.QualityScore = await CalculateQualityScoreAsync(response, cancellationToken);

        return metrics;
    }

    private ValidationResult ValidateRequest(DocumentationRequest request)
    {
        var errors = new List<string>();

        if (request == null)
        {
            errors.Add("La solicitud no puede ser nula");
            return new ValidationResult { IsValid = false, Errors = errors };
        }

        if (string.IsNullOrWhiteSpace(request.CodeSnippet))
        {
            errors.Add("El código no puede estar vacío");
        }
        else if (request.CodeSnippet.Length > MaxDocumentationLength)
        {
            errors.Add($"El código excede el límite de {MaxDocumentationLength} caracteres");
        }

        if (request.ScopeLines < 1 || request.ScopeLines > 100)
        {
            errors.Add("El rango de líneas debe estar entre 1 y 100");
        }

        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }

    private async Task<DocumentationContentResult> GenerateDocumentationContentAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;
        var sections = new List<string>();
        
        var codeAnalysis = await AnalyzeCodeStructureAsync(request.CodeSnippet, cancellationToken);
        sections.Add(GenerateSummarySection(request.CodeSnippet, request.Comment));
        
        var parameterDocs = await GenerateParameterDocumentationAsync(request.CodeSnippet, cancellationToken);
        if (!string.IsNullOrEmpty(parameterDocs))
        {
            sections.Add(parameterDocs);
        }

        var returnDocs = await GenerateReturnDocumentationAsync(request.CodeSnippet, cancellationToken);
        if (!string.IsNullOrEmpty(returnDocs))
        {
            sections.Add(returnDocs);
        }

        var usageExample = GenerateUsageExample(request.CodeSnippet);
        sections.Add(usageExample);

        var processingTime = DateTime.UtcNow - startTime;
        var confidenceScore = CalculateConfidenceScore(codeAnalysis, sections.Count);

        return new DocumentationContentResult
        {
            GeneratedContent = string.Join("\n\n", sections),
            Sections = sections,
            ConfidenceScore = confidenceScore,
            ProcessingTime = processingTime
        };
    }

    private async Task<CodeAnalysisResult> AnalyzeCodeStructureAsync(
        string codeSnippet,
        CancellationToken cancellationToken)
    {
        var result = new CodeAnalysisResult
        {
            HasMethods = codeSnippet.Contains("public") || codeSnippet.Contains("private"),
            HasClasses = codeSnippet.Contains("class"),
            HasInterfaces = codeSnippet.Contains("interface"),
            HasParameters = codeSnippet.Contains("(") && codeSnippet.Contains(")"),
            HasReturnType = codeSnippet.Contains("return") || codeSnippet.Contains(": "),
            LineCount = codeSnippet.Split('\n').Length
        };

        await Task.CompletedTask;
        return result;
    }

    private string GenerateSummarySection(string codeSnippet, string comment)
    {
        var summaryBuilder = new System.Text.StringBuilder();
        summaryBuilder.AppendLine("## Resumen");
        
        if (!string.IsNullOrWhiteSpace(comment))
        {
            summaryBuilder.AppendLine($"Descripción basada en el comentario proporcionado: {comment}");
        }
        else
        {
            var codeType = DetectCodeType(codeSnippet);
            summaryBuilder.AppendLine($"Este código implementa un componente de tipo: {codeType}");
        }

        return summaryBuilder.ToString();
    }

    private async Task<string> GenerateParameterDocumentationAsync(
        string codeSnippet,
        CancellationToken cancellationToken)
    {
        var paramRegex = new System.Text.RegularExpressions.Regex(@"\((.*?)\)");
        var match = paramRegex.Match(codeSnippet);

        if (!match.Success)
        {
            return string.Empty;
        }

        var parameters = match.Groups[1].Value.Split(',')
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrEmpty(p) && !p.Contains("="))
            .ToList();

        if (parameters.Count == 0)
        {
            return string.Empty;
        }

        var docBuilder = new System.Text.StringBuilder();
        docBuilder.AppendLine("## Parámetros");

        foreach (var param in parameters)
        {
            var paramName = ExtractParameterName(param);
            var paramType = ExtractParameterType(param);
            docBuilder.AppendLine($"- **{paramName}** ({paramType}): Parámetro de entrada del método");
        }

        await Task.CompletedTask;
        return docBuilder.ToString();
    }

    private async Task<string> GenerateReturnDocumentationAsync(
        string codeSnippet,
        CancellationToken cancellationToken)
    {
        var returnTypes = new[] { "Task<", "void", "string", "int", "bool", "object", "IEnumerable<", "IActionResult" };
        var detectedReturnType = returnTypes.FirstOrDefault(rt => codeSnippet.Contains(rt));

        if (string.IsNullOrEmpty(detectedReturnType))
        {
            return string.Empty;
        }

        var docBuilder = new System.Text.StringBuilder();
        docBuilder.AppendLine("## Retorno");
        docBuilder.AppendLine($"- **Tipo**: {detectedReturnType}");
        docBuilder.AppendLine("- **Descripción**: Valor retornado por la operación");

        await Task.CompletedTask;
        return docBuilder.ToString();
    }

    private string GenerateUsageExample(string codeSnippet)
    {
        var exampleBuilder = new System.Text.StringBuilder();
        exampleBuilder.AppendLine("## Ejemplo de Uso");
        
        var methodName = ExtractMethodName(codeSnippet);
        if (!string.IsNullOrEmpty(methodName))
        {
            exampleBuilder.AppendLine("```csharp");
            exampleBuilder.AppendLine($"// Llamada al método");
            exampleBuilder.AppendLine($"var result = {methodName}(/* parámetros */);");
            exampleBuilder.AppendLine("```");
        }

        return exampleBuilder.ToString();
    }

    private string DetectCodeType(string codeSnippet)
    {
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Controller"))
            return "Controlador MVC";
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Service"))
            return "Servicio de aplicación";
        if (codeSnippet.Contains("interface ") || codeSnippet.Contains("abstract "))
            return "Contrato/Interfaz";
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Entity"))
            return "Entidad de dominio";
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Repository"))
            return "Repositorio";
        if (codeSnippet.Contains("class "))
            return "Clase";
        
        return "Código";
    }

    private string ExtractMethodName(string codeSnippet)
    {
        var methodRegex = new System.Text.RegularExpressions.Regex(@"(?:public|private|protected)\s+(?:async\s+)?(?:\w+(?:<[^>]+>)?)\s+(\w+)\s*\(");
        var match = methodRegex.Match(codeSnippet);
        return match.Success ? match.Groups[1].Value : "methodName";
    }

    private string ExtractParameterName(string parameter)
    {
        var parts = parameter.Split(' ');
        return parts.Length > 1 ? parts[^1] : parameter;
    }

    private string ExtractParameterType(string parameter)
    {
        var parts = parameter.Split(' ');
        return parts.Length > 0 ? parts[0] : "object";
    }

    private bool IsDocumentableLine(string line)
    {
        var documentableKeywords = new[] { "public", "private", "protected", "class", "interface", "void", "Task", "string", "int", "bool" };
        return documentableKeywords.Any(kw => line.Contains(kw));
    }

    private DocumentationSectionType DetermineSectionType(string line)
    {
        if (line.Contains("class ") || line.Contains("interface "))
            return DocumentationSectionType.TypeDeclaration;
        if (line.Contains("public ") || line.Contains("private "))
            return DocumentationSectionType.Member;
        if (line.Contains("//") || line.Contains("/*"))
            return DocumentationSectionType.Comment;
        
        return DocumentationSectionType.Other;
    }

    private async Task<string> AnalyzeLineForDocumentationAsync(string line, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (line.Contains("class "))
            return "Definición de clase";
        if (line.Contains("interface "))
            return "Definición de interfaz";
        if (line.Contains("public ") && line.Contains("("))
            return "Método público";
        
        return "Línea de código";
    }

    private double CalculateConfidenceScore(CodeAnalysisResult analysis, int sectionsCount)
    {
        var baseScore = 0.5;
        
        if (analysis.HasMethods) baseScore += 0.15;
        if (analysis.HasClasses) baseScore += 0.1;
        if (analysis.HasParameters) baseScore += 0.15;
        if (analysis.HasReturnType) baseScore += 0.1;
        
        return Math.Min(1.0, baseScore);
    }

    private int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    private int CountLines(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        return text.Split('\n').Length;
    }

    private double CalculateCompletenessScore(DocumentationMetrics metrics)
    {
        var score = 0.0;
        
        if (metrics.TotalCharacters > 100) score += 0.3;
        if (metrics.TotalWords > 20) score += 0.3;
        if (metrics.TotalLines > 5) score += 0.2;
        if (metrics.ConfidenceScore > 0.7) score += 0.2;
        
        return score;
    }

    private async Task<double> CalculateQualityScoreAsync(
        DocumentationResponse response,
        CancellationToken cancellationToken)
    {
        var qualityScore = 0.5;
        
        if (!string.IsNullOrWhiteSpace(response.Documentation))
        {
            qualityScore += 0.2;
            
            if (response.Documentation.Contains("##"))
            {
                qualityScore += 0.15;
            }
            
            if (response.Documentation.Contains("```"))
            {
                qualityScore += 0.15;
            }
        }

        await Task.CompletedTask;
        return Math.Min(1.0, qualityScore);
    }

    private class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    private class DocumentationContentResult
    {
        public string GeneratedContent { get; set; } = string.Empty;
        public List<string> Sections { get; set; } = new();
        public double ConfidenceScore { get; set; }
        public TimeSpan ProcessingTime { get; set; }
    }

    private class CodeAnalysisResult
    {
        public bool HasMethods { get; set; }
        public bool HasClasses { get; set; }
        public bool HasInterfaces { get; set; }
        public bool HasParameters { get; set; }
        public bool HasReturnType { get; set; }
        public int LineCount { get; set; }
    }
}

public enum DocumentationSectionType
{
    TypeDeclaration,
    Member,
    Comment,
    Other
}

public class DocumentationSection
{
    public int LineNumber { get; set; }
    public string Content { get; set; } = string.Empty;
    public DocumentationSectionType SectionType { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class DocumentationMetrics
{
    public int TotalCharacters { get; set; }
    public int TotalWords { get; set; }
    public int TotalLines { get; set; }
    public double ConfidenceScore { get; set; }
    public double CompletenessScore { get; set; }
    public double QualityScore { get; set; }
    public DateTime GeneratedAt { get; set; }
    public double ProcessingTimeMs { get; set; }
}