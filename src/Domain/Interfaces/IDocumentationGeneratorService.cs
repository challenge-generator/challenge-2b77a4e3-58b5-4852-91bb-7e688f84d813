using Domain.Models;

namespace Domain.Interfaces;

/// <summary>
/// Define el contrato para la generación automática de documentación
/// a partir de comentarios y código fuente.
/// </summary>
public interface IDocumentationGeneratorService
{
    /// <summary>
    /// Genera documentación automáticamente a partir de un fragmento de código
    /// y sus comentarios asociados.
    /// </summary>
    /// <param name="request">Solicitud con el código y comentarios a documentar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con la documentación generada.</returns>
    /// <exception cref="DocumentationGenerationException">Cuando la generación de documentación falla.</exception>
    Task<DocumentationResponse> GenerateDocumentationAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Analiza el código fuente y extrae los elementos que requieren documentación.
    /// </summary>
    /// <param name="codeSnippet">Código fuente a analizar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Colección de elementos que necesitan documentación.</returns>
    Task<IEnumerable<DocumentationElement>> AnalyzeCodeForDocumentationAsync(
        string codeSnippet,
        CancellationToken cancellationToken);

    /// <summary>
    /// Valida que la documentación generada cumpla con los estándares de calidad.
    /// </summary>
    /// <param name="documentation">Documentación a validar.</param>
    /// <returns>Resultado de la validación con problemas encontrados.</returns>
    DocumentationValidationResult ValidateDocumentation(string documentation);

    /// <summary>
    /// Estima el nivel de detalle de documentación que se puede generar
    /// basándose en la complejidad del código.
    /// </summary>
    /// <param name="codeSnippet">Código a analizar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Nivel de detalle estimado (1-5).</returns>
    Task<int> EstimateDocumentationLevelAsync(
        string codeSnippet,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representa un elemento de código que requiere documentación.
/// </summary>
public class DocumentationElement
{
    public string ElementName { get; set; } = string.Empty;
    public DocumentationElementType ElementType { get; set; }
    public int StartLine { get; set; }
    public int EndLine { get; set; }
    public bool HasExistingDocumentation { get; set; }
    public string ExistingDocumentation { get; set; } = string.Empty;
    public List<string> Parameters { get; set; } = new();
    public string ReturnType { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
}

/// <summary>
/// Tipos de elementos de código que pueden requerir documentación.
/// </summary>
public enum DocumentationElementType
{
    Class,
    Interface,
    Method,
    Property,
    Field,
    Constructor,
    Enum,
    Struct,
    Delegate,
    Event
}

/// <summary>
/// Resultado de la validación de documentación.
/// </summary>
public class DocumentationValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public List<string> ValidationWarnings { get; set; } = new();
    public double QualityScore { get; set; }

    public bool HasErrors() => ValidationErrors.Count > 0;
    public bool HasWarnings() => ValidationWarnings.Count > 0;

    public string GetSummary()
    {
        var summary = $"Documentation validation: {(IsValid ? "PASSED" : "FAILED")}";
        if (HasErrors())
            summary += $" | Errors: {string.Join(", ", ValidationErrors)}";
        if (HasWarnings())
            summary += $" | Warnings: {string.Join(", ", ValidationWarnings)}";
        summary += $" | Quality Score: {QualityScore:P0}";
        return summary;
    }
}