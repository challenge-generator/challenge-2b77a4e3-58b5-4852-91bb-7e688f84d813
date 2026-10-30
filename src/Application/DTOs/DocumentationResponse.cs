using System;
using System.Collections.Generic;

namespace Application.DTOs;

public class DocumentationResponse
{
    public string OriginalCode { get; set; } = string.Empty;
    public string GeneratedDocumentation { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public double ConfidenceScore { get; set; }
    public List<string> AppliedTransformations { get; set; } = new();
    public Dictionary<string, string> Metadata { get; set; } = new();

    public DocumentationResponse()
    {
        GeneratedAt = DateTime.UtcNow;
    }

    public DocumentationResponse(string originalCode, string generatedDocumentation, string language) : this()
    {
        OriginalCode = originalCode;
        GeneratedDocumentation = generatedDocumentation;
        Language = language;
        IsSuccess = !string.IsNullOrWhiteSpace(generatedDocumentation);
        ConfidenceScore = IsSuccess ? CalculateInitialConfidence() : 0.0;
    }

    private double CalculateInitialConfidence()
    {
        if (string.IsNullOrWhiteSpace(GeneratedDocumentation))
            return 0.0;

        double baseConfidence = 0.5;
        if (GeneratedDocumentation.Length > 50)
            baseConfidence += 0.2;
        if (GeneratedDocumentation.Contains("\n"))
            baseConfidence += 0.1;
        if (Metadata.Count > 0)
            baseConfidence += 0.1;

        return Math.Min(baseConfidence, 0.9);
    }

    public bool RequiresManualReview()
    {
        return ConfidenceScore < 0.7 || !string.IsNullOrWhiteSpace(ErrorMessage);
    }

    public string GetSummary()
    {
        if (!IsSuccess)
            return $"Documentation generation failed: {ErrorMessage}";

        return $"Documentation generated successfully for {Language} code. " +
               $"Confidence: {ConfidenceScore:P0}. Transformations applied: {AppliedTransformations.Count}";
    }

    public void AddTransformation(string transformation)
    {
        if (!string.IsNullOrWhiteSpace(transformation) && !AppliedTransformations.Contains(transformation))
        {
            AppliedTransformations.Add(transformation);
        }
    }

    public void SetMetadata(string key, string value)
    {
        if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
        {
            Metadata[key] = value;
        }
    }

    public string GetFormattedOutput()
    {
        var header = new string('=', 60);
        var output = $"{header}\n" +
                     $"DOCUMENTATION RESPONSE\n" +
                     $"{header}\n" +
                     $"Status: {(IsSuccess ? "SUCCESS" : "FAILED")}\n" +
                     $"Generated: {GeneratedAt:yyyy-MM-dd HH:mm:ss} UTC\n" +
                     $"Language: {Language}\n" +
                     $"Confidence: {ConfidenceScore:P1}\n";

        if (!string.IsNullOrWhiteSpace(ErrorMessage))
        {
            output += $"\nError: {ErrorMessage}\n";
        }

        output += $"\n{header}\n" +
                  $"GENERATED DOCUMENTATION\n" +
                  $"{header}\n" +
                  $"{GeneratedDocumentation}\n";

        if (AppliedTransformations.Count > 0)
        {
            output += $"\n{header}\n" +
                      $"TRANSFORMATIONS APPLIED\n" +
                      $"{header}\n";
            foreach (var transformation in AppliedTransformations)
            {
                output += $"- {transformation}\n";
            }
        }

        return output;
    }
}