using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Domain.Models;

public class CodeCorrectionRequest
{
    [Required]
    public string ProblematicCode { get; }

    [Required]
    public string ErrorDescription { get; }

    [Range(0, 2)]
    public int SeverityLevel { get; }

    public CodeCorrectionRequest(string problematicCode, string errorDescription, int severityLevel = 1)
    {
        if (string.IsNullOrWhiteSpace(problematicCode))
            throw new ArgumentException("Problematic code cannot be empty or whitespace.", nameof(problematicCode));

        if (string.IsNullOrWhiteSpace(errorDescription))
            throw new ArgumentException("Error description cannot be empty or whitespace.", nameof(errorDescription));

        if (severityLevel < 0 || severityLevel > 2)
            throw new ArgumentOutOfRangeException(nameof(severityLevel), "Severity level must be between 0 and 2.");

        ProblematicCode = problematicCode;
        ErrorDescription = errorDescription;
        SeverityLevel = severityLevel;
    }

    public class Validator : AbstractValidator<CodeCorrectionRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ProblematicCode).NotEmpty().WithMessage("Problematic code is required.");
            RuleFor(x => x.ErrorDescription).NotEmpty().WithMessage("Error description is required.");
            RuleFor(x => x.SeverityLevel).InclusiveBetween(0, 2).WithMessage("Severity level must be between 0 and 2.");
        }
    }
}

public class CodeCorrectionException : Exception
{
    public CodeCorrectionException(string message) : base(message) { }
    public CodeCorrectionException(string message, Exception innerException) : base(message, innerException) { }
}