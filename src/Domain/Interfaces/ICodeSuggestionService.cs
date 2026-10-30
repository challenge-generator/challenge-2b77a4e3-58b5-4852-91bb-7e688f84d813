namespace Domain.Interfaces;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

/// <summary>
/// Contrato definido por el dominio para el servicio que genera sugerencias de código utilizando IA.
/// </summary>
public interface ICodeSuggestionService
{
    /// <summary>
    /// Genera sugerencias de código basadas en el contexto actual y la descripción proporcionada.
    /// </summary>
    /// <param name="request">Solicitud que contiene el código actual y el contexto.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con las sugerencias generadas.</returns>
    /// <exception cref="CodeSuggestionException">Se lanza cuando ocurre un error durante la generación.</exception>
    Task<CodeSuggestionResponse> GenerateSuggestionsAsync(
        CodeSuggestionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Valida la calidad de las sugerencias generadas según criterios específicos del dominio.
    /// </summary>
    /// <param name="suggestions">Lista de sugerencias a validar.</param>
    /// <returns>Diccionario con los resultados de la validación para cada sugerencia.</returns>
    IDictionary<CodeSuggestion, bool> ValidateSuggestions(IEnumerable<CodeSuggestion> suggestions);

    /// <summary>
    /// Obtiene métricas de rendimiento de las sugerencias generadas recientemente.
    /// </summary>
    /// <param name="limit">Número máximo de registros a retornar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de métricas de rendimiento.</returns>
    Task<IEnumerable<SuggestionMetrics>> GetPerformanceMetricsAsync(
        int limit = 10,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Representa métricas de rendimiento para las sugerencias de código.
/// </summary>
public class SuggestionMetrics
{
    public double AverageConfidenceScore { get; set; }
    public int TotalSuggestionsGenerated { get; set; }
    public int HighConfidenceSuggestions { get; set; }
    public TimeSpan AverageGenerationTime { get; set; }
}