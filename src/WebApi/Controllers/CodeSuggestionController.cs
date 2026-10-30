using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeSuggestionController : ControllerBase
{
    private readonly ICodeSuggestionService _suggestionService;
    private readonly ILogger<CodeSuggestionController> _logger;
    private readonly IMediator _mediator;

    public CodeSuggestionController(
        ICodeSuggestionService suggestionService,
        ILogger<CodeSuggestionController> logger,
        IMediator mediator)
    {
        _suggestionService = suggestionService ?? throw new ArgumentNullException(nameof(suggestionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("generate")]
    [ProducesResponseType(typeof(CodeSuggestionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CodeSuggestionResponse>> GenerateSuggestions(
        [FromBody] CodeSuggestionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _logger.LogWarning("Se recibió una solicitud nula de sugerencias de código");
            return BadRequest(new { message = "La solicitud no puede ser nula" });
        }

        try
        {
            _logger.LogInformation("Generando sugerencias de código para contexto: {ContextDescription}", 
                request.ContextDescription);

            var response = await _suggestionService.GenerateSuggestionsAsync(request, cancellationToken);

            if (!response.HasValidSuggestions())
            {
                _logger.LogWarning("No se generaron sugerencias válidas para el contexto proporcionado");
                return Ok(new CodeSuggestionResponse(
                    Enumerable.Empty<CodeSuggestion>(), 
                    request.ContextDescription));
            }

            _logger.LogInformation("Se generaron {Count} sugerencias de código", 
                response.Suggestions.Count());
            return Ok(response);
        }
        catch (CodeSuggestionException ex)
        {
            _logger.LogError(ex, "Error al generar sugerencias de código: {Message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { 
                message = "Error al procesar la solicitud de sugerencias",
                detail = ex.Message 
            });
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("La operación de generación de sugerencias fue cancelada");
            return StatusCode(StatusCodes.Status499ClientClosedRequest, new 
            { message = "La solicitud fue cancelada" });
        }
    }

    [HttpPost("validate")]
    [ProducesResponseType(typeof(Dictionary<string, bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Dictionary<string, bool>> ValidateSuggestions(
        [FromBody] IEnumerable<CodeSuggestion> suggestions)
    {
        if (suggestions == null || !suggestions.Any())
        {
            return BadRequest(new { message = "Debe proporcionar al menos una sugerencia para validar" });
        }

        _logger.LogInformation("Validando {Count} sugerencias de código", suggestions.Count());
        var validationResults = _suggestionService.ValidateSuggestions(suggestions);

        var result = validationResults.ToDictionary(
            kvp => kvp.Key.Id.ToString(),
            kvp => kvp.Value);

        return Ok(result);
    }

    [HttpGet("metrics")]
    [ProducesResponseType(typeof(IEnumerable<SuggestionMetrics>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SuggestionMetrics>>> GetPerformanceMetrics(
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Obteniendo métricas de rendimiento de sugerencias");
            var metrics = await _suggestionService.GetPerformanceMetricsAsync(cancellationToken);
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener métricas de rendimiento");
            return StatusCode(StatusCodes.Status500InternalServerError, new 
            { message = "Error al recuperar métricas" });
        }
    }

    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}