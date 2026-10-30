using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeCorrectionController : ControllerBase
{
    private readonly ICodeCorrectionService _correctionService;
    private readonly ILogger<CodeCorrectionController> _logger;
    private readonly IMediator _mediator;

    public CodeCorrectionController(
        ICodeCorrectionService correctionService,
        ILogger<CodeCorrectionController> logger,
        IMediator mediator)
    {
        _correctionService = correctionService ?? throw new ArgumentNullException(nameof(correctionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("correct")]
    [ProducesResponseType(typeof(CodeCorrectionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CodeCorrectionResponse>> CorrectCode(
        [FromBody] CodeCorrectionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _logger.LogWarning("Se recibió una solicitud nula de corrección de código");
            return BadRequest(new { message = "La solicitud no puede ser nula" });
        }

        if (string.IsNullOrWhiteSpace(request.ProblematicCode))
        {
            _logger.LogWarning("Se recibió código problemático vacío o nulo");
            return BadRequest(new { message = "El código problemático no puede estar vacío" });
        }

        try
        {
            _logger.LogInformation("Iniciando corrección de código con severidad: {SeverityLevel}", 
                request.SeverityLevel);

            var response = await _correctionService.CorrectCodeAsync(request, cancellationToken);

            if (response.RequiresManualReview())
            {
                _logger.LogWarning("La corrección requiere revisión manual. Puntuación de confianza: {Confidence}", 
                    response.ConfidenceScore);
            }
            else
            {
                _logger.LogInformation("Corrección completada exitosamente con confianza: {Confidence}", 
                    response.ConfidenceScore);
            }

            return Ok(response);
        }
        catch (CodeCorrectionException ex)
        {
            _logger.LogError(ex, "Error al corregir código: {Message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { 
                message = "Error al procesar la corrección de código",
                detail = ex.Message 
            });
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("La operación de corrección de código fue cancelada");
            return StatusCode(StatusCodes.Status499ClientClosedRequest, new 
            { message = "La solicitud fue cancelada" });
        }
    }

    [HttpPost("analyze")]
    [ProducesResponseType(typeof(IEnumerable<CodeIssue>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CodeIssue>>> AnalyzeCode(
        [FromBody] CodeCorrectionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.ProblematicCode))
        {
            return BadRequest(new { message = "Debe proporcionar código para analizar" });
        }

        try
        {
            _logger.LogInformation("Analizando código para identificar problemas");
            var issues = await _correctionService.AnalyzeCodeAsync(request, cancellationToken);
            
            var issuesList = issues.ToList();
            var criticalCount = issuesList.Count(i => i.IsCritical());
            
            _logger.LogInformation("Se identificaron {Total} problemas, {Critical} críticos", 
                issuesList.Count, criticalCount);

            return Ok(issues);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al analizar el código");
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { message = "Error al analizar el código" });
        }
    }

    [HttpGet("complexity/{issueId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> EstimateComplexity(
        [FromRoute] string issueId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(issueId))
        {
            return BadRequest(new { message = "El ID del problema no puede estar vacío" });
        }

        try
        {
            _logger.LogInformation("Estimando complejidad de corrección para problema: {IssueId}", issueId);
            var complexity = await _correctionService.EstimateCorrectionComplexityAsync(issueId, cancellationToken);
            return Ok(new { issueId, complexity });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Problema no encontrado: {IssueId}", issueId);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al estimar complejidad");
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { message = "Error al calcular la complejidad" });
        }
    }

    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}