using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Domain.Models;

public class DocumentationRequest
{
    [Required]
    public string CodeSnippet { get; }

    [Required]
    public string Comment { get; }

    [Range(1, 100)]
    public int ScopeLines { get; }

    public DocumentationRequest(string codeSnippet, string comment, int scopeLines = 10)
    {
        if (string.IsNullOrWhiteSpace(codeSnippet))
            throw new ArgumentException("Code snippet cannot be empty or whitespace.", nameof(codeSnippet));

        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be empty or whitespace.", nameof(comment));

        if (scopeLines < 1 || scopeLines > 100)
            throw new ArgumentOutOfRangeException(nameof(scopeLines), "Scope lines must be between 1 and 100.");

        CodeSnippet = codeSnippet;
        Comment = comment;
        ScopeLines = scopeLines;
    }

    public class Validator : AbstractValidator<DocumentationRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CodeSnippet).NotEmpty().WithMessage("Code snippet is required.");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is required.");
            RuleFor(x => x.ScopeLines).InclusiveBetween(1, 100).WithMessage("Scope lines must be between 1 and 100.");
        }
    }
}

public class DocumentationGenerationException : Exception
{
    public DocumentationGenerationException(string message) : base(message) { }
    public DocumentationGenerationException(string message, Exception innerException) : base(message, innerException) { }
}