using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CodeSuggestionResponse
{
    [Required]
    public IReadOnlyList<SuggestionDto> Suggestions { get; set; } = Array.Empty<SuggestionDto>();

    [Required]
    public string OriginalContext { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public string? ServiceProvider { get; set; }

    public double ProcessingTimeMs { get; set; }

    public ResponseMetadata? Metadata { get; set; }

    public CodeSuggestionResponse()
    {
    }

    public CodeSuggestionResponse(IEnumerable<SuggestionDto> suggestions, string originalContext)
    {
        Suggestions = suggestions?.ToList() ?? new List<SuggestionDto>();
        OriginalContext = originalContext ?? throw new ArgumentNullException(nameof(originalContext));
    }

    public bool HasValidSuggestions()
    {
        return Suggestions != null && Suggestions.Any(s => !string.IsNullOrWhiteSpace(s.SuggestedCode));
    }

    public int GetValidSuggestionCount()
    {
        return Suggestions?.Count(s => !string.IsNullOrWhiteSpace(s.SuggestedCode)) ?? 0;
    }

    public IEnumerable<SuggestionDto> GetHighConfidenceSuggestions(double threshold = 0.7)
    {
        return Suggestions?.Where(s => s.ConfidenceScore >= threshold) ?? Enumerable.Empty<SuggestionDto>();
    }

    public SuggestionDto? GetBestSuggestion()
    {
        return Suggestions?.OrderByDescending(s => s.ConfidenceScore).FirstOrDefault();
    }

    public void AddMetadata(string key, string value)
    {
        Metadata ??= new ResponseMetadata();
        Metadata.CustomData ??= new Dictionary<string, string>();
        Metadata.CustomData[key] = value;
    }

    public string GetSummary()
    {
        var validCount = GetValidSuggestionCount();
        return $"Se generaron {validCount} sugerencias válidas de {Suggestions?.Count ?? 0} totales. Tiempo de procesamiento: {ProcessingTimeMs:F2}ms";
    }
}

public class SuggestionDto
{
    [Required]
    [StringLength(10000, MinimumLength = 1)]
    public string SuggestedCode { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Explanation { get; set; } = string.Empty;

    [Range(0.0, 1.0)]
    public double ConfidenceScore { get; set; }

    [StringLength(500)]
    public string Category { get; set; } = string.Empty;

    public List<string> AppliedPatterns { get; set; } = new();

    public Dictionary<string, string>? Context { get; set; }

    public int? LineNumber { get; set; }

    public SuggestionDto()
    {
    }

    public SuggestionDto(string suggestedCode, string explanation, double confidenceScore)
    {
        SuggestedCode = suggestedCode ?? throw new ArgumentNullException(nameof(suggestedCode));
        Explanation = explanation ?? string.Empty;
        ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
    }

    public bool IsHighConfidence() => ConfidenceScore >= 0.8;

    public bool IsMediumConfidence() => ConfidenceScore >= 0.5 && ConfidenceScore < 0.8;

    public bool IsLowConfidence() => ConfidenceScore < 0.5;

    public string GetFormattedSuggestion()
    {
        var confidence = IsHighConfidence() ? "Alta" : (IsMediumConfidence() ? "Media" : "Baja");
        return $"[Confianza {confidence}: {ConfidenceScore:P0}]\n{Explanation}\n\nCódigo:\n{SuggestedCode}";
    }
}

public class ResponseMetadata
{
    public Dictionary<string, string>? CustomData { get; set; }

    public string? RequestId { get; set; }

    public int TotalTokens { get; set; }

    public string? ModelVersion { get; set; }
}