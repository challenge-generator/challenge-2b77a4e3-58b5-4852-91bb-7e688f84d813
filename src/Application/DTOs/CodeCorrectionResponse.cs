using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CodeCorrectionResponse
{
    public string CorrectedCode { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public List<string> AppliedFixes { get; set; } = new();
    public double ConfidenceScore { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = string.Empty;
    public TimeSpan ProcessingDuration { get; set; }
    public List<CorrectionImpact> ImpactAnalysis { get; set; } = new();
    public Dictionary<string, string> Metadata { get; set; } = new();

    public CodeCorrectionResponse() { }

    public CodeCorrectionResponse(
        string correctedCode, 
        string explanation, 
        IEnumerable<string> appliedFixes, 
        double confidenceScore)
    {
        CorrectedCode = correctedCode ?? throw new ArgumentNullException(nameof(correctedCode));
        Explanation = explanation ?? string.Empty;
        AppliedFixes = appliedFixes?.ToList() ?? new List<string>();
        ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
        IsSuccess = !string.IsNullOrWhiteSpace(correctedCode);
        ProcessedAt = DateTime.UtcNow;
    }

    public bool RequiresManualReview()
    {
        return ConfidenceScore < 0.7 || IsSuccess == false || 
               AppliedFixes.Count == 0 || ImpactAnalysis.Any(i => i.BreaksExistingTests);
    }

    public string GetSummary()
    {
        var status = IsSuccess ? "Éxito" : "Fallido";
        var confidence = $"{(ConfidenceScore * 100):F1}%";
        var fixes = $"{AppliedFixes.Count} corrección(es) aplicada(s)";
        return $"{status} - Confianza: {confidence} - {fixes}";
    }

    public bool HasHighConfidence() => ConfidenceScore >= 0.8;
    
    public bool HasAppliedFixes() => AppliedFixes != null && AppliedFixes.Count > 0;
    
    public IEnumerable<string> GetCriticalFixes()
    {
        return ImpactAnalysis
            .Where(i => i.Severity == ImpactSeverity.Critical)
            .Select(i => i.Description);
    }

    public CodeCorrectionResponse WithRequestId(string requestId)
    {
        RequestId = requestId;
        return this;
    }

    public CodeCorrectionResponse WithProcessingDuration(TimeSpan duration)
    {
        ProcessingDuration = duration;
        return this;
    }
}

public class CorrectionImpact
{
    public string Description { get; set; } = string.Empty;
    public ImpactSeverity Severity { get; set; }
    public bool BreaksExistingTests { get; set; }
    public string? AffectedComponent { get; set; }
    public List<string> RelatedFiles { get; set; } = new();

    public CorrectionImpact() { }

    public CorrectionImpact(string description, ImpactSeverity severity, bool breaksTests = false)
    {
        Description = description;
        Severity = severity;
        BreaksExistingTests = breaksTests;
    }
}

public enum ImpactSeverity
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}