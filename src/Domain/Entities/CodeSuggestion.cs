namespace Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

/// <summary>
/// Representa una sugerencia de código generada por la IA para mejorar o completar el código existente.
/// </summary>
public class CodeSuggestion
{
    public Guid Id { get; private set; }

    [Required]
    [StringLength(10000, MinimumLength = 1)]
    public string SuggestedCode { get; private set; }

    [Required]
    [Range(0.0, 1.0)]
    public double ConfidenceScore { get; private set; }

    [StringLength(500)]
    public string Explanation { get; private set; }

    public DateTime GeneratedAt { get; private set; }

    [Required]
    public CodeSuggestionRequest OriginalRequest { get; private set; }

    public IEnumerable<string> AppliedRules { get; private set; }

    public CodeSuggestion(
        string suggestedCode,
        double confidenceScore,
        string explanation,
        CodeSuggestionRequest originalRequest,
        IEnumerable<string> appliedRules)
    {
        Id = Guid.NewGuid();
        SuggestedCode = suggestedCode ?? throw new ArgumentNullException(nameof(suggestedCode));
        ConfidenceScore = confidenceScore;
        Explanation = explanation;
        GeneratedAt = DateTime.UtcNow;
        OriginalRequest = originalRequest ?? throw new ArgumentNullException(nameof(originalRequest));
        AppliedRules = appliedRules ?? throw new ArgumentNullException(nameof(appliedRules));

        Validate();
    }

    private void Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);

        if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
        {
            throw new ValidationException(string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
        }
    }

    public bool IsHighConfidence() => ConfidenceScore >= 0.8;

    public string GetFormattedSuggestion()
    {
        return $@"Sugerencia generada el {GeneratedAt:yyyy-MM-dd HH:mm:ss}:
Confianza: {ConfidenceScore:P0}
Explicación: {Explanation}

Código sugerido:
{SuggestedCode}";
    }

    public bool Equals(CodeSuggestion other)
    {
        if (other is null) return false;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object obj) => Equals(obj as CodeSuggestion);

    public override int GetHashCode() => Id.GetHashCode();
}