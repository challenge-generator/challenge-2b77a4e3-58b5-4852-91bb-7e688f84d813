using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.DTOs;

public class CodeSuggestionRequest
{
    [Required(ErrorMessage = "El código actual es requerido")]
    [StringLength(50000, MinimumLength = 1, ErrorMessage = "El código debe tener entre 1 y 50000 caracteres")]
    public string CurrentCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción del contexto es requerida")]
    [StringLength(2000, MinimumLength = 1, ErrorMessage = "El contexto debe tener entre 1 y 2000 caracteres")]
    public string ContextDescription { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "El número de sugerencias debe estar entre 1 y 10")]
    public int SuggestionsCount { get; set; } = 3;

    public string? Language { get; set; }

    public string? Framework { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public CodeSuggestionRequest()
    {
    }

    public CodeSuggestionRequest(string currentCode, string contextDescription, int suggestionsCount = 3)
    {
        CurrentCode = currentCode ?? throw new ArgumentNullException(nameof(currentCode));
        ContextDescription = contextDescription ?? throw new ArgumentNullException(nameof(contextDescription));
        SuggestionsCount = suggestionsCount;
    }

    public void Normalize()
    {
        if (string.IsNullOrWhiteSpace(Language))
        {
            Language = "csharp";
        }

        Metadata ??= new Dictionary<string, string>();

        if (!Metadata.ContainsKey("timestamp"))
        {
            Metadata["timestamp"] = DateTime.UtcNow.ToString("O");
        }

        if (!Metadata.ContainsKey("requestId"))
        {
            Metadata["requestId"] = Guid.NewGuid().ToString();
        }
    }

    public string GetCacheKey()
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var input = $"{CurrentCode}|{ContextDescription}|{SuggestionsCount}|{Language}|{Framework}";
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool IsValidForProcessing()
    {
        return !string.IsNullOrWhiteSpace(CurrentCode) &&
               !string.IsNullOrWhiteSpace(ContextDescription) &&
               SuggestionsCount >= 1 && SuggestionsCount <= 10;
    }
}

public class CodeSuggestionRequestValidator : AbstractValidator<CodeSuggestionRequest>
{
    public CodeSuggestionRequestValidator()
    {
        RuleFor(x => x.CurrentCode)
            .NotEmpty().WithMessage("El código actual es requerido")
            .MaximumLength(50000).WithMessage("El código no puede exceder 50000 caracteres")
            .MinimumLength(1).WithMessage("El código debe tener al menos 1 carácter");

        RuleFor(x => x.ContextDescription)
            .NotEmpty().WithMessage("La descripción del contexto es requerida")
            .MaximumLength(2000).WithMessage("El contexto no puede exceder 2000 caracteres")
            .MinimumLength(1).WithMessage("El contexto debe tener al menos 1 carácter");

        RuleFor(x => x.SuggestionsCount)
            .InclusiveBetween(1, 10).WithMessage("El número de sugerencias debe estar entre 1 y 10");

        RuleFor(x => x.Language)
            .MaximumLength(50).WithMessage("El lenguaje no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.Framework)
            .MaximumLength(50).WithMessage("El framework no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Framework));
    }
}