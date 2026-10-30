namespace Domain.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

/// <summary>
/// Contrato definido por el dominio para el servicio que corrige automáticamente errores en el código.
/// </summary>
public interface ICodeCorrectionService
{
    /// <summary>
    /// Corrige automáticamente errores en el código basado en la descripción del problema.
    /// </summary>
    /// <param name="request">Solicitud que contiene el código problemático y la descripción del error.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el código corregido y explicación.</returns>
    /// <exception cref="CodeCorrectionException">Se lanza cuando ocurre un error durante la corrección.</exception>
    Task<CodeCorrectionResponse> CorrectCodeAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Analiza el código para identificar posibles problemas sin aplicar correcciones.
    /// </summary>
    /// <param name="code">Código a analizar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de problemas identificados en el código.</returns>
    Task<IEnumerable<CodeIssue>> AnalyzeCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Estima la complejidad de la corrección necesaria para un fragmento de código.
    /// </summary>
    /// <param name="code">Código a evaluar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Nivel estimado de complejidad (0-10).</returns>
    Task<int> EstimateCorrectionComplexityAsync(
        string code,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Representa un problema identificado en el código durante el análisis.
/// </summary>
public class CodeIssue
{
    public string IssueId { get; set; }
    public string Description { get; set; }
    public int Severity { get; set; } // 0: Baja, 1: Media, 2: Alta
    public int LineNumber { get; set; }
    public string SuggestedFix { get; set; }

    public bool IsCritical() => Severity >= 2;
}