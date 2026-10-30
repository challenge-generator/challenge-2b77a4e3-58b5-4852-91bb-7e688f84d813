using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models;

public class CodeCorrectionResponse
{
    public string CorrectedCode { get; }
    public string Explanation { get; }
    public List<string> AppliedFixes { get; }
    public DateTime CorrectedAt { get; }
    public double ConfidenceScore { get; }
    public bool IsSuccess => !string.IsNullOrWhiteSpace(CorrectedCode);

    public CodeCorrectionResponse(string correctedCode, string explanation, IEnumerable<string> appliedFixes, double confidenceScore)
    {
        if (confidenceScore < 0 || confidenceScore > 1)
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), "Confidence score must be between 0 and 1.");

        CorrectedCode = correctedCode ?? throw new ArgumentNullException(nameof(correctedCode));
        Explanation = explanation ?? string.Empty;
        AppliedFixes = appliedFixes?.ToList() ?? new List<string>();
        CorrectedAt = DateTime.UtcNow;
        ConfidenceScore = confidenceScore;
    }

    public bool RequiresManualReview()
    {
        return ConfidenceScore < 0.7 || AppliedFixes.Contains("PotentialBreakingChange");
    }

    public string GetSummary()
    {
        return IsSuccess
            ? $"Correction successful ({ConfidenceScore:P0}). Applied {AppliedFixes.Count} fixes."
            : "Correction failed - manual review required.";
    }
}