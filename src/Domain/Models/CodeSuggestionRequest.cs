using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Domain.Models;

public class CodeSuggestionRequest
{
    [Required]
    public string CurrentCode { get; }

    [Required]
    public string ContextDescription { get; }

    [Range(1, 5)]
    public int SuggestionsCount { get; }

    public CodeSuggestionRequest(string currentCode, string contextDescription, int suggestionsCount = 3)
    {
        if (string.IsNullOrWhiteSpace(currentCode))
            throw new ArgumentException("Current code cannot be empty or whitespace.", nameof(currentCode));

        if (string.IsNullOrWhiteSpace(contextDescription))
            throw new ArgumentException("Context description cannot be empty or whitespace.", nameof(contextDescription));

        if (suggestionsCount < 1 || suggestionsCount > 5)
            throw new ArgumentOutOfRangeException(nameof(suggestionsCount), "Suggestions count must be between 1 and 5.");

        CurrentCode = currentCode;
        ContextDescription = contextDescription;
        SuggestionsCount = suggestionsCount;
    }

    public class Validator : AbstractValidator<CodeSuggestionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CurrentCode).NotEmpty().WithMessage("Current code is required.");
            RuleFor(x => x.ContextDescription).NotEmpty().WithMessage("Context description is required.");
            RuleFor(x => x.SuggestionsCount).InclusiveBetween(1, 5).WithMessage("Suggestions count must be between 1 and 5.");
        }
    }
}

public class CodeSuggestionException : Exception
{
    public CodeSuggestionException(string message) : base(message) { }
    public CodeSuggestionException(string message, Exception innerException) : base(message, innerException) { }
}