using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.DTOs;

public class DocumentationRequest
{
    public string CodeSnippet { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int ScopeLines { get; set; } = 10;
    public DocumentationFormat Format { get; set; } = DocumentationFormat.Markdown;
    public string? Language { get; set; }
    public bool IncludeExamples { get; set; } = true;
    public bool IncludeApiReference { get; set; } = false;
    public string? TargetAudience { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = Guid.NewGuid().ToString();

    public DocumentationRequest() { }

    public DocumentationRequest(string codeSnippet, string comment, int scopeLines = 10)
    {
        if (string.IsNullOrWhiteSpace(codeSnippet))
            throw new ArgumentException("El fragmento de código no puede estar vacío", nameof(codeSnippet));

        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("El comentario no puede estar vacío", nameof(comment));

        CodeSnippet = codeSnippet;
        Comment = comment;
        ScopeLines = scopeLines;
    }

    public bool IsValidScope() => ScopeLines > 0 && ScopeLines <= 500;
    
    public int GetEstimatedProcessingTime() => Math.Max(1, (CodeSnippet.Length / 500) + (ScopeLines / 20));
    
    public IDictionary<string, object> ToMetadata()
    {
        return new Dictionary<string, object>
        {
            { "RequestId", RequestId },
            { "RequestedAt", RequestedAt },
            { "ScopeLines", ScopeLines },
            { "Format", Format },
            { "Language", Language ?? "auto-detect" },
            { "IncludeExamples", IncludeExamples },
            { "CodeLength", CodeSnippet.Length }
        };
    }

    public string GetDocumentationContext()
    {
        var contextParts = new List<string>();
        
        if (!string.IsNullOrEmpty(Language))
            contextParts.Add($"Lenguaje: {Language}");
        
        contextParts.Add($"Líneas de alcance: {ScopeLines}");
        contextParts.Add($"Formato: {Format}");
        
        if (IncludeExamples)
            contextParts.Add("Incluir ejemplos: Sí");
            
        return string.Join(" | ", contextParts);
    }
}

public class DocumentationRequestValidator : AbstractValidator<DocumentationRequest>
{
    public DocumentationRequestValidator()
    {
        RuleFor(x => x.CodeSnippet)
            .NotEmpty()
            .WithMessage("El fragmento de código es requerido")
            .MinimumLength(1)
            .WithMessage("El código debe tener al menos 1 carácter")
            .MaximumLength(100000)
            .WithMessage("El código no puede exceder 100000 caracteres");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage("El comentario es requerido")
            .MinimumLength(3)
            .WithMessage("El comentario debe tener al menos 3 caracteres")
            .MaximumLength(5000)
            .WithMessage("El comentario no puede exceder 5000 caracteres");

        RuleFor(x => x.ScopeLines)
            .InclusiveBetween(1, 500)
            .WithMessage("Las líneas de alcance deben estar entre 1 y 500");

        RuleFor(x => x.Language)
            .MaximumLength(50)
            .WithMessage("El lenguaje no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.TargetAudience)
            .MaximumLength(100)
            .WithMessage("La audiencia objetivo no puede exceder 100 caracteres")
            .When(x => !string.IsNullOrEmpty(x.TargetAudience));

        RuleFor(x => x.Format)
            .IsInEnum()
            .WithMessage("El formato de documentación debe ser válido");
    }
}

public enum DocumentationFormat
{
    Markdown = 0,
    Html = 1,
    PlainText = 2,
    OpenApi = 3,
    XmlDoc = 4,
    Json = 5
}

public class DocumentationRequestValidationException : Exception
{
    public IEnumerable<string> ValidationErrors { get; }
    
    public DocumentationRequestValidationException(string message, IEnumerable<string> errors) 
        : base(message)
    {
        ValidationErrors = errors;
    }
}