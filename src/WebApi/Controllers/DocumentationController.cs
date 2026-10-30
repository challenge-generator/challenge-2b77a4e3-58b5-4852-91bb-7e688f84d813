using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.DTOs;
using Domain.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DocumentationController : ControllerBase
{
    private readonly IDocumentationGeneratorService _documentationService;
    private readonly ILogger<DocumentationController> _logger;
    private const int MaxRequestSize = 50000;
    private const int DefaultScopeLines = 10;

    public DocumentationController(
        IDocumentationGeneratorService documentationService,
        ILogger<DocumentationController> logger)
    {
        _documentationService = documentationService ?? throw new ArgumentNullException(nameof(documentationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("generate")]
    [ProducesResponseType(typeof(DocumentationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateDocumentation(
        [FromBody] DocumentationRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _logger.LogWarning("Documentation request received with null body");
            return BadRequest(new { error = "Request body is required" });
        }

        if (string.IsNullOrWhiteSpace(request.CodeSnippet))
        {
            _logger.LogWarning("Documentation request received with empty code snippet");
            return BadRequest(new { error = "Code snippet is required" });
        }

        if (request.CodeSnippet.Length > MaxRequestSize)
        {
            _logger.LogWarning("Documentation request exceeds maximum size: {Size}", request.CodeSnippet.Length);
            return BadRequest(new { error = $"Code snippet exceeds maximum size of {MaxRequestSize} characters" });
        }

        try
        {
            _logger.LogInformation("Generating documentation for code snippet of length {Length}", 
                request.CodeSnippet.Length);

            var response = await _documentationService.GenerateDocumentationAsync(
                request.CodeSnippet,
                request.Comment ?? string.Empty,
                request.ScopeLines > 0 ? request.ScopeLines : DefaultScopeLines,
                cancellationToken);

            _logger.LogInformation("Documentation generated successfully");
            return Ok(response);
        }
        catch (DocumentationGenerationException ex)
        {
            _logger.LogError(ex, "Failed to generate documentation: {Message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { 
                error = "Documentation generation failed",
                details = ex.Message 
            });
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Documentation generation was cancelled");
            return StatusCode(StatusCodes.Status499ClientClosedRequest, new 
            { 
                error = "Request was cancelled" 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during documentation generation");
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { 
                error = "An unexpected error occurred" 
            });
        }
    }

    [HttpGet("health")]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok(new HealthCheckResponse 
        { 
            Status = "Healthy", 
            Service = "Documentation", 
            Timestamp = DateTime.UtcNow 
        });
    }

    [HttpPost("validate")]
    [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult ValidateDocumentation([FromBody] ValidationRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Documentation))
        {
            return BadRequest(new { error = "Documentation content is required" });
        }

        var validationResult = new ValidationResponse
        {
            IsValid = !string.IsNullOrWhiteSpace(request.Documentation),
            Issues = new List<string>()
        };

        if (request.Documentation.Length < 10)
        {
            validationResult.IsValid = false;
            validationResult.Issues.Add("Documentation is too short");
        }

        return Ok(validationResult);
    }

    private class HealthCheckResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    private class ValidationRequest
    {
        public string Documentation { get; set; } = string.Empty;
    }

    private class ValidationResponse
    {
        public bool IsValid { get; set; }
        public List<string> Issues { get; set; } = new();
    }
}