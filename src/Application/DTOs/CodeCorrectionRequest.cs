using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.DTOs;

public class CodeCorrectionRequest
{
    public string ProblematicCode { get; set; } = string.Empty;
    public string ErrorDescription { get; set; } = string.Empty;
    public int SeverityLevel { get; set; } = 1;
    public string? Language { get; set; }
    public string? Framework { get; set; }
    public Dictionary<string, string>? AdditionalContext { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = Guid.NewGuid().ToString();

    public CodeCorrectionRequest() { }

    public CodeCorrectionRequest(string problematicCode, string errorDescription, int severityLevel = 1)
    {
        if (string.IsNullOrWhiteSpace(problematicCode))
            throw new ArgumentException("El código problemático no puede estar vacío", nameof(problematicCode));
        
        if (string.IsNullOrWhiteSpace(errorDescription))
            throw new ArgumentException("La descripción del error no puede estar vacía", nameof(errorDescription));

        ProblematicCode = problematicCode;
        ErrorDescription = errorDescription;
        SeverityLevel = severityLevel;
    }

    public bool IsValidSeverity() => SeverityLevel >= 0 && SeverityLevel <= 2;
    
    public string GetSummary() => $"Corrección solicitada para código de {ProblematicCode.Length} caracteres con severidad {SeverityLevel}";
    
    public bool RequiresUrgency() => SeverityLevel >= 2;
    
    public IDictionary<string, object> ToMetadata()
    {
        return new Dictionary<string, object>
        {
            { "RequestId", RequestId },
            { "RequestedAt", RequestedAt },
            { "SeverityLevel", SeverityLevel },
            { "Language", Language ?? "unknown" },
            { "CodeLength", ProblematicCode.Length }
        };
    }
}

public class CodeCorrectionRequestValidator : AbstractValidator<CodeCorrectionRequest>
{
    public CodeCorrectionRequestValidator()
    {
        RuleFor(x => x.ProblematicCode)
            .NotEmpty()
            .WithMessage("El código problemático es requerido")
            .MinimumLength(1)
            .WithMessage("El código debe tener al menos 1 carácter")
            .MaximumLength(50000)
            .WithMessage("El código no puede exceder 50000 caracteres");

        RuleFor(x => x.ErrorDescription)
            .NotEmpty()
            .WithMessage("La descripción del error es requerida")
            .MinimumLength(5)
            .WithMessage("La descripción debe tener al menos 5 caracteres")
            .MaximumLength(2000)
            .WithMessage("La descripción no puede exceder 2000 caracteres");

        RuleFor(x => x.SeverityLevel)
            .InclusiveBetween(0, 2)
            .WithMessage("El nivel de severidad debe estar entre 0 y 2");

        RuleFor(x => x.Language)
            .MaximumLength(50)
            .WithMessage("El lenguaje no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.Framework)
            .MaximumLength(50)
            .WithMessage("El framework no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Framework));
    }
}

public class CodeCorrectionRequestValidationException : Exception
{
    public IEnumerable<string> ValidationErrors { get; }
    
    public CodeCorrectionRequestValidationException(string message, IEnumerable<string> errors) 
        : base(message)
    {
        ValidationErrors = errors;
    }
}