using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models;

public class CodeSuggestionResponse
{
    public IReadOnlyList<CodeSuggestion> Suggestions { get; }
    public string OriginalContext { get; }
    public DateTime GeneratedAt { get; }

    public CodeSuggestionResponse(IEnumerable<CodeSuggestion> suggestions, string originalContext)
    {
        if (suggestions == null)
            throw new ArgumentNullException(nameof(suggestions));

        if (string.IsNullOrWhiteSpace(originalContext))
            throw new ArgumentException("Original context cannot be empty or whitespace.", nameof(originalContext));

        Suggestions = suggestions.ToList().AsReadOnly();
        OriginalContext = originalContext;
        GeneratedAt = DateTime.UtcNow;
    }

    public bool HasValidSuggestions() => Suggestions.Any(s => !string.IsNullOrWhiteSpace(s.SuggestedCode));

    public class CodeSuggestion
    {
        public string SuggestedCode { get; }
        public string Description { get; }
        public double ConfidenceScore { get; }
        public List<string> RelevantTags { get; }

        public CodeSuggestion(string suggestedCode, string description, double confidenceScore, IEnumerable<string> relevantTags = null)
        {
            if (string.IsNullOrWhiteSpace(suggestedCode))
                throw new ArgumentException("Suggested code cannot be empty or whitespace.", nameof(suggestedCode));

            if (confidenceScore < 0 || confidenceScore > 1)
                throw new ArgumentOutOfRangeException(nameof(confidenceScore), "Confidence score must be between 0 and 1.");

            SuggestedCode = suggestedCode;
            Description = description ?? string.Empty;
            ConfidenceScore = confidenceScore;
            RelevantTags = relevantTags?.ToList() ?? new List<string>();
        }
    }
}