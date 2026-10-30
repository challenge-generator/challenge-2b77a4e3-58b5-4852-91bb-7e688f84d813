# Prompt para Mejorar el Codigo Base

Copia y pega el contenido del bloque de abajo en un asistente de IA (Claude, ChatGPT)
para obtener un ZIP con el proyecto completo y arrancable.

Si preferis trabajar en tu editor con un agente local (Claude Code, Cursor, Copilot), usa `AGENTS.md` en vez de este archivo: dice lo mismo pero para que escriba los archivos en disco.

## Las dos reglas que no se negocian

1. **Completa el boilerplate.** Todo lo que el proyecto necesita para compilar y arrancar: manifiesto de dependencias, punto de entrada, configuracion, capa de interfaz, y las capas del patron arquitectonico declarado. Eso es andamiaje y es tu trabajo.
2. **NO resuelvas el reto.** Los entregables de las fases son el trabajo de la persona. El hueco pedagogico se deja como esta: el proyecto arranca, pero lo que el reto pide implementar NO esta implementado.

Dicho de otra forma: si algo impide compilar, arreglalo. Si algo es logica de negocio incompleta, validaciones ausentes, un secreto hardcodeado o un patron mejorable, dejalo exactamente como esta — es lo que la persona tiene que encontrar.

## Superficie de practica — NO resuelvas

Estos archivos SON el ejercicio de la persona. No los implementes; deja stubs.

- `tests/Application.Tests/Services/CodeSuggestionServiceTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- `tests/Infrastructure.Tests/Services/AwsCodeWhispererServiceTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- `tests/Application.Tests/Application.Tests.csproj` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- `tests/Infrastructure.Tests/Infrastructure.Tests.csproj` — El topic pide TDD/pruebas: este archivo es el ejercicio.

## Como saber que terminaste

```bash
dotnet build
```

Ese comando corriendo sin errores es la definicion de "listo".

---

```
## Briefing del reto (autoridad)
Este bloque manda sobre los archivos adjuntos. El stack y el rol salen de AQUÍ, no de un topic genérico ni de markdown placeholder.

### Perfil
Chapter Backend, Especialidad Desarrollador, Tecnología .Net, Advanced

### Brecha de conocimiento
Ha trabajado con algún asistente AI en desarrollo (Amazon Q Dev, Kiro, Github Copilot) y lo utiliza en su día a día como desarrollador. Cierre de brechas de conocimiento en el uso efectivo de herramientas de IA para acelerar el desarrollo, mejorar la calidad del código y aumentar la productividad

### Misión / candidato
Candidato con experiencia avanzada en Backend .Net, buscando optimizar su flujo de trabajo con IA generativa

### Reto
- Tema: Desarrollo
- Seniority: advanced-l2
- Tipo: practical
- Título: Optimización de Flujo de Trabajo con IA Generativa en Desarrollo Backend .Net
- Tiempo estimado: 10 horas

### Fases (trabajo del HUMANO — PROHIBIDO completarlas)
No implementes estos entregables. Dejalos como hueco pedagógico. El asistente solo materializa el proyecto arrancable para que el participante pueda trabajar.
- Fase 1: Integración Inicial de la Herramienta de IA — objetivo: Incorporar la herramienta de IA en el entorno de desarrollo para proporcionar sugerencias de código. — entregable (NO resolver): Configuración de la herramienta de IA en el IDE con sugerencias de código en tiempo real.
- Fase 2: Automatización de Correcciones de Código — objetivo: Implementar un sistema que automatice la corrección de errores comunes en el código. — entregable (NO resolver): Sistema automatizado que corrige errores comunes en el código utilizando la herramienta de IA.
- Fase 3: Generación de Documentación a partir de Comentarios — objetivo: Configurar la herramienta de IA para generar documentación a partir de comentarios en el código. — entregable (NO resolver): Sistema que genera documentación automáticamente a partir de comentarios en el código utilizando la herramienta de IA.

Eres un asistente experto en análisis, corrección y generación de archivos de cualquier tipo:
código fuente, documentación, hojas de cálculo, documentos Word, configuraciones, entre otros.
Voy a enviarte una cadena de texto que contiene uno o más archivos. Cada archivo está delimitado por un marcador con el siguiente formato:
// === ARCHIVO: ruta/del/archivo.extension ===
o también puede aparecer como:
## === ARCHIVO: ruta/del/archivo.extension ===
Lo que sigue al marcador puede ser:

El contenido real del archivo (código, texto, YAML, etc.)
Una descripción en lenguaje natural de lo que debe contener el archivo


TU TAREA
PASO 0 — ¿Esto es un proyecto o una carcasa?
Antes de extraer archivos, leé el Briefing (si está) y diagnosticá el adjunto.

Es CARCASA si ocurre CUALQUIERA de estas:
- No hay manifiesto de dependencias del stack del briefing (manifest.json de VTEX IO / package.json / pom.xml / build.gradle / requirements.txt / go.mod / *.tf / *.csproj, según corresponda)
- Hay un "binario" que en realidad es un comentario ("no puede ser mostrado como texto plano", placeholder .fig/.docx vacío)
- Los markdowns ya completan entregables de fases posteriores ("se implementó fade-in", lista de áreas ya resuelta)

Si es CARCASA:
- MATERIALIZÁ un proyecto que arranca en el stack del briefing (VTEX IO Store Framework, Angular, Terraform, pytest, Nest, etc.). Incluí manifiesto, punto de entrada y capa de interfaz reales.
- NO copies los markdowns de "solución" como si fueran el producto. Son ruido de generación.
- NO resuelvas las fases del briefing (están marcadas PROHIBIDO). Dejá el hueco pedagógico: el flujo existe, las microinteracciones/calidad/infra que el reto pide NO están hechas.
- Después seguí al PASO 5 (ZIP).

Si es un proyecto REAL (manifiesto + código que compila o arranca):
- Seguí PASO 1 en adelante. 🔴 compilación sí. 🟡 pedagógico no.

PASO 1 — Detección y extracción
Identifica todos los archivos presentes en la cadena. Para cada archivo extrae:

Su ruta completa (ej: src/main/java/com/pragma/Service.java)
Su contenido o descripción

PASO 2 — Clasificación por tipo
Clasifica cada archivo en una de estas categorías:
A) Código fuente (Java, Python, TypeScript, JavaScript, Kotlin, etc.)
B) Configuración / documentación (YAML, properties, Markdown, JSON, txt, etc.)
C) Excel (.xlsx, .xls, .csv)
D) Word (.docx, .doc)
E) Otro tipo de archivo binario o especial
PASO 3 — Clasificación de errores en código fuente

Objetivo prioritario: que el proyecto compile. No corrijas flujo de negocio ni lógica funcional.

Antes de modificar cualquier archivo de código fuente, clasifica cada problema encontrado en una de estas dos categorías:
🔴 ERROR DE COMPILACIÓN — corregir siempre
Son errores que impiden que el proyecto arranque, sin valor pedagógico:

Import faltante o incorrecto
Clase, método o variable referenciada que no existe en ningún archivo del proyecto
Error de sintaxis
Anotación con atributos inválidos
Dependencia ausente en pom.xml, package.json, etc.
Archivo referenciado que no existe y debe ser creado con implementación mínima

→ CORREGIR estos errores.
🟡 PROBLEMA FUNCIONAL O DE CALIDAD — preservar siempre
Son problemas que no impiden compilar. Pueden ser intencionales para el aprendizaje:

Clave secreta hardcodeada ("secret", "password123")
API deprecada que funciona pero tiene reemplazo moderno
Lógica de negocio incorrecta o incompleta
Código redundante o de baja legibilidad
Falta de validaciones en flujo de negocio
Patrones de diseño incorrectos pero funcionales
Concurrencia no segura
Configuración funcional pero no óptima

→ PRESERVAR tal cual. No corregir, no mejorar, no comentar.
PASO 4 — Procesamiento según tipo de archivo
Tipo A — Código fuente
Aplica únicamente las correcciones clasificadas como 🔴 ERROR DE COMPILACIÓN.
No alteres ningún elemento clasificado como 🟡 PROBLEMA FUNCIONAL O DE CALIDAD.
Si falta un archivo referenciado, créalo con la implementación mínima necesaria para compilar.
Tipo B — Configuración / documentación
Extrae el contenido tal cual, sin modificaciones salvo errores evidentes de sintaxis
(ej: YAML mal indentado).
Tipo C — Excel (.xlsx)
Si viene con contenido real, genera el archivo respetando ese contenido.
Si viene con descripción en lenguaje natural, genera un archivo Excel funcional con:

Fila de encabezados en negrita con color de fondo distintivo
Columnas con ancho ajustado al contenido
Tipos de dato correctos por columna
Validaciones si la descripción lo indica
Hojas nombradas descriptivamente si hay más de una
Filas de ejemplo si no hay datos reales

Tipo D — Word (.docx)
Si viene con contenido real, genera el archivo respetando ese contenido.
Si viene con descripción en lenguaje natural, genera un documento Word funcional con:

Estilos de título (Título 1, Título 2) para jerarquía de secciones
Fuente legible (Calibri o equivalente), tamaño 11-12pt para cuerpo
Márgenes estándar
Tabla de contenido si tiene múltiples secciones
Tablas con encabezados en negrita si aplica

Tipo E — Otro
Genera el archivo con el contenido o estructura más apropiada según la descripción.
PASO 5 — Exportación en ZIP
Empaqueta todos los archivos en un único archivo ZIP descargable respetando exactamente
la estructura de rutas indicada por los marcadores.
El ZIP debe incluir:

Archivos de código con únicamente los errores de compilación corregidos
Archivos de configuración y documentación sin cambios
Archivos nuevos creados para resolver dependencias de compilación faltantes
Archivos Excel y Word generados desde descripción

IMPORTANTE: El ZIP debe estar listo para descargar al finalizar. No preguntes si el usuario
quiere generarlo. Simplemente genera el archivo y proporciona el enlace de descarga; No debes desplegar en el chat el resumen de lo que arreglaste al Zip, solo entregalo.

REGLAS IMPORTANTES

No omitas ningún archivo aunque no tenga errores ni modificaciones
Respeta los nombres y rutas exactas indicadas por los marcadores
Si un archivo no tiene marcador claro, infiere el nombre desde su contenido
Si la cadena contiene solo documentación, placeholders o binarios fake, NO la reproduzcas:
aplicá PASO 0 (materializar el proyecto del briefing). Reproducir la carcasa es un fallo.
No agregues texto después del enlace de descarga del ZIP
No preguntes si el usuario quiere el ZIP: simplemente generalo siempre
Si detectas que falta un archivo de configuración necesario para compilar
(pom.xml, package.json, requirements.txt, build.gradle, etc.), créalo e inclúyelo
inferiendo su contenido desde los imports y frameworks detectados en el código
Nunca corrijas problemas 🟡 aunque parezcan obvios o fáciles de mejorar.
El participante que recibirá este proyecto los debe encontrar y resolver él mismo.


INPUT
Aquí está la cadena con los archivos:

// === ARCHIVO: src/WebApi/Program.cs ===
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    typeof(Domain.Models.CodeSuggestionRequest).Assembly
));

// Configuración de AWS CodeWhisperer
builder.Services.AddScoped<IDocumentationGeneratorService, AwsCodeWhispererService>();
builder.Services.AddScoped<ICodeSuggestionService, AwsCodeWhispererService>();
builder.Services.AddScoped<ICodeCorrectionService, AwsCodeWhispererService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();

namespace WebApi
{
    public partial class Program { }
}

// === ARCHIVO: src/WebApi/appsettings.json ===
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AWS": {
    "CodeWhisperer": {
      "Region": "us-east-1",
      "AccessKey": "your-access-key",
      "SecretKey": "your-secret-key",
      "ServiceUrl": "https://codewhisperer.us-east-1.amazonaws.com"
    }
  },
  "Cors": {
    "AllowedOrigins": [ "*" ]
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CodeOptimizationAI;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

// === ARCHIVO: src/Domain/Domain.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="MediatR" Version="12.5.0" />
    <PackageReference Include="FluentValidation" Version="11.9.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Application\Application.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Compile Update="Models\*.cs">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Compile>
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Interfaces" />
    <Folder Include="Models" />
    <Folder Include="Exceptions" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/Domain/Models/DocumentationRequest.cs ===
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

// === ARCHIVO: src/Domain/Models/CodeSuggestionRequest.cs ===
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

// === ARCHIVO: src/Domain/Models/CodeSuggestionResponse.cs ===
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

// === ARCHIVO: src/Domain/Models/CodeCorrectionRequest.cs ===
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

// === ARCHIVO: src/Domain/Models/CodeCorrectionResponse.cs ===
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models;

public class CodeCorrectionResponse
{
    public string CorrectedCode { get; }
    public string Explanation { get; }
    public List<string> AppliedFixes { get; }
    public DateTime CorrectedAt { get; }
    public double ConfidenceScore { get; }
    public bool IsSuccess => !string.IsNullOrWhiteSpace(CorrectedCode);

    public CodeCorrectionResponse(string correctedCode, string explanation, IEnumerable<string> appliedFixes, double confidenceScore)
    {
        if (confidenceScore < 0 || confidenceScore > 1)
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), "Confidence score must be between 0 and 1.");

        CorrectedCode = correctedCode ?? throw new ArgumentNullException(nameof(correctedCode));
        Explanation = explanation ?? string.Empty;
        AppliedFixes = appliedFixes?.ToList() ?? new List<string>();
        CorrectedAt = DateTime.UtcNow;
        ConfidenceScore = confidenceScore;
    }

    public bool RequiresManualReview()
    {
        return ConfidenceScore < 0.7 || AppliedFixes.Contains("PotentialBreakingChange");
    }

    public string GetSummary()
    {
        return IsSuccess
            ? $"Correction successful ({ConfidenceScore:P0}). Applied {AppliedFixes.Count} fixes."
            : "Correction failed - manual review required.";
    }
}


// === ARCHIVO: src/Domain/Entities/CodeSuggestion.cs ===
namespace Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

/// <summary>
/// Representa una sugerencia de código generada por la IA para mejorar o completar el código existente.
/// </summary>
public class CodeSuggestion
{
    public Guid Id { get; private set; }

    [Required]
    [StringLength(10000, MinimumLength = 1)]
    public string SuggestedCode { get; private set; }

    [Required]
    [Range(0.0, 1.0)]
    public double ConfidenceScore { get; private set; }

    [StringLength(500)]
    public string Explanation { get; private set; }

    public DateTime GeneratedAt { get; private set; }

    [Required]
    public CodeSuggestionRequest OriginalRequest { get; private set; }

    public IEnumerable<string> AppliedRules { get; private set; }

    public CodeSuggestion(
        string suggestedCode,
        double confidenceScore,
        string explanation,
        CodeSuggestionRequest originalRequest,
        IEnumerable<string> appliedRules)
    {
        Id = Guid.NewGuid();
        SuggestedCode = suggestedCode ?? throw new ArgumentNullException(nameof(suggestedCode));
        ConfidenceScore = confidenceScore;
        Explanation = explanation;
        GeneratedAt = DateTime.UtcNow;
        OriginalRequest = originalRequest ?? throw new ArgumentNullException(nameof(originalRequest));
        AppliedRules = appliedRules ?? throw new ArgumentNullException(nameof(appliedRules));

        Validate();
    }

    private void Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);

        if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
        {
            throw new ValidationException(string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
        }
    }

    public bool IsHighConfidence() => ConfidenceScore >= 0.8;

    public string GetFormattedSuggestion()
    {
        return $@"Sugerencia generada el {GeneratedAt:yyyy-MM-dd HH:mm:ss}:
Confianza: {ConfidenceScore:P0}
Explicación: {Explanation}

Código sugerido:
{SuggestedCode}";
    }

    public bool Equals(CodeSuggestion other)
    {
        if (other is null) return false;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object obj) => Equals(obj as CodeSuggestion);

    public override int GetHashCode() => Id.GetHashCode();
}

// === ARCHIVO: src/Domain/Interfaces/ICodeSuggestionService.cs ===
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

// === ARCHIVO: src/Domain/Interfaces/ICodeCorrectionService.cs ===
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


// === ARCHIVO: src/Application/Application.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="MediatR" Version="12.5.0" />
    <PackageReference Include="FluentValidation" Version="11.9.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Domain\Domain.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="DTOs" />
    <Folder Include="Services" />
    <Folder Include="Commands" />
    <Folder Include="Queries" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/Application/DTOs/CodeSuggestionRequest.cs ===
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.DTOs;

public class CodeSuggestionRequest
{
    [Required(ErrorMessage = "El código actual es requerido")]
    [StringLength(50000, MinimumLength = 1, ErrorMessage = "El código debe tener entre 1 y 50000 caracteres")]
    public string CurrentCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción del contexto es requerida")]
    [StringLength(2000, MinimumLength = 1, ErrorMessage = "El contexto debe tener entre 1 y 2000 caracteres")]
    public string ContextDescription { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "El número de sugerencias debe estar entre 1 y 10")]
    public int SuggestionsCount { get; set; } = 3;

    public string? Language { get; set; }

    public string? Framework { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public CodeSuggestionRequest()
    {
    }

    public CodeSuggestionRequest(string currentCode, string contextDescription, int suggestionsCount = 3)
    {
        CurrentCode = currentCode ?? throw new ArgumentNullException(nameof(currentCode));
        ContextDescription = contextDescription ?? throw new ArgumentNullException(nameof(contextDescription));
        SuggestionsCount = suggestionsCount;
    }

    public void Normalize()
    {
        if (string.IsNullOrWhiteSpace(Language))
        {
            Language = "csharp";
        }

        Metadata ??= new Dictionary<string, string>();

        if (!Metadata.ContainsKey("timestamp"))
        {
            Metadata["timestamp"] = DateTime.UtcNow.ToString("O");
        }

        if (!Metadata.ContainsKey("requestId"))
        {
            Metadata["requestId"] = Guid.NewGuid().ToString();
        }
    }

    public string GetCacheKey()
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var input = $"{CurrentCode}|{ContextDescription}|{SuggestionsCount}|{Language}|{Framework}";
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool IsValidForProcessing()
    {
        return !string.IsNullOrWhiteSpace(CurrentCode) &&
               !string.IsNullOrWhiteSpace(ContextDescription) &&
               SuggestionsCount >= 1 && SuggestionsCount <= 10;
    }
}

public class CodeSuggestionRequestValidator : AbstractValidator<CodeSuggestionRequest>
{
    public CodeSuggestionRequestValidator()
    {
        RuleFor(x => x.CurrentCode)
            .NotEmpty().WithMessage("El código actual es requerido")
            .MaximumLength(50000).WithMessage("El código no puede exceder 50000 caracteres")
            .MinimumLength(1).WithMessage("El código debe tener al menos 1 carácter");

        RuleFor(x => x.ContextDescription)
            .NotEmpty().WithMessage("La descripción del contexto es requerida")
            .MaximumLength(2000).WithMessage("El contexto no puede exceder 2000 caracteres")
            .MinimumLength(1).WithMessage("El contexto debe tener al menos 1 carácter");

        RuleFor(x => x.SuggestionsCount)
            .InclusiveBetween(1, 10).WithMessage("El número de sugerencias debe estar entre 1 y 10");

        RuleFor(x => x.Language)
            .MaximumLength(50).WithMessage("El lenguaje no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.Framework)
            .MaximumLength(50).WithMessage("El framework no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Framework));
    }
}

// === ARCHIVO: src/Application/DTOs/CodeSuggestionResponse.cs ===
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CodeSuggestionResponse
{
    [Required]
    public IReadOnlyList<SuggestionDto> Suggestions { get; set; } = Array.Empty<SuggestionDto>();

    [Required]
    public string OriginalContext { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public string? ServiceProvider { get; set; }

    public double ProcessingTimeMs { get; set; }

    public ResponseMetadata? Metadata { get; set; }

    public CodeSuggestionResponse()
    {
    }

    public CodeSuggestionResponse(IEnumerable<SuggestionDto> suggestions, string originalContext)
    {
        Suggestions = suggestions?.ToList() ?? new List<SuggestionDto>();
        OriginalContext = originalContext ?? throw new ArgumentNullException(nameof(originalContext));
    }

    public bool HasValidSuggestions()
    {
        return Suggestions != null && Suggestions.Any(s => !string.IsNullOrWhiteSpace(s.SuggestedCode));
    }

    public int GetValidSuggestionCount()
    {
        return Suggestions?.Count(s => !string.IsNullOrWhiteSpace(s.SuggestedCode)) ?? 0;
    }

    public IEnumerable<SuggestionDto> GetHighConfidenceSuggestions(double threshold = 0.7)
    {
        return Suggestions?.Where(s => s.ConfidenceScore >= threshold) ?? Enumerable.Empty<SuggestionDto>();
    }

    public SuggestionDto? GetBestSuggestion()
    {
        return Suggestions?.OrderByDescending(s => s.ConfidenceScore).FirstOrDefault();
    }

    public void AddMetadata(string key, string value)
    {
        Metadata ??= new ResponseMetadata();
        Metadata.CustomData ??= new Dictionary<string, string>();
        Metadata.CustomData[key] = value;
    }

    public string GetSummary()
    {
        var validCount = GetValidSuggestionCount();
        return $"Se generaron {validCount} sugerencias válidas de {Suggestions?.Count ?? 0} totales. Tiempo de procesamiento: {ProcessingTimeMs:F2}ms";
    }
}

public class SuggestionDto
{
    [Required]
    [StringLength(10000, MinimumLength = 1)]
    public string SuggestedCode { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Explanation { get; set; } = string.Empty;

    [Range(0.0, 1.0)]
    public double ConfidenceScore { get; set; }

    [StringLength(500)]
    public string Category { get; set; } = string.Empty;

    public List<string> AppliedPatterns { get; set; } = new();

    public Dictionary<string, string>? Context { get; set; }

    public int? LineNumber { get; set; }

    public SuggestionDto()
    {
    }

    public SuggestionDto(string suggestedCode, string explanation, double confidenceScore)
    {
        SuggestedCode = suggestedCode ?? throw new ArgumentNullException(nameof(suggestedCode));
        Explanation = explanation ?? string.Empty;
        ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
    }

    public bool IsHighConfidence() => ConfidenceScore >= 0.8;

    public bool IsMediumConfidence() => ConfidenceScore >= 0.5 && ConfidenceScore < 0.8;

    public bool IsLowConfidence() => ConfidenceScore < 0.5;

    public string GetFormattedSuggestion()
    {
        var confidence = IsHighConfidence() ? "Alta" : (IsMediumConfidence() ? "Media" : "Baja");
        return $"[Confianza {confidence}: {ConfidenceScore:P0}]\n{Explanation}\n\nCódigo:\n{SuggestedCode}";
    }
}

public class ResponseMetadata
{
    public Dictionary<string, string>? CustomData { get; set; }

    public string? RequestId { get; set; }

    public int TotalTokens { get; set; }

    public string? ModelVersion { get; set; }
}


// === ARCHIVO: src/Application/DTOs/CodeCorrectionRequest.cs ===
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.DTOs;

public class CodeCorrectionRequest
{
    public string ProblematicCode { get; set; } = string.Empty;
    public string ErrorDescription { get; set; } = string.Empty;
    public int SeverityLevel { get; set; } = 1;
    public string? Language { get; set; }
    public string? Framework { get; set; }
    public Dictionary<string, string>? AdditionalContext { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = Guid.NewGuid().ToString();

    public CodeCorrectionRequest() { }

    public CodeCorrectionRequest(string problematicCode, string errorDescription, int severityLevel = 1)
    {
        if (string.IsNullOrWhiteSpace(problematicCode))
            throw new ArgumentException("El código problemático no puede estar vacío", nameof(problematicCode));
        
        if (string.IsNullOrWhiteSpace(errorDescription))
            throw new ArgumentException("La descripción del error no puede estar vacía", nameof(errorDescription));

        ProblematicCode = problematicCode;
        ErrorDescription = errorDescription;
        SeverityLevel = severityLevel;
    }

    public bool IsValidSeverity() => SeverityLevel >= 0 && SeverityLevel <= 2;
    
    public string GetSummary() => $"Corrección solicitada para código de {ProblematicCode.Length} caracteres con severidad {SeverityLevel}";
    
    public bool RequiresUrgency() => SeverityLevel >= 2;
    
    public IDictionary<string, object> ToMetadata()
    {
        return new Dictionary<string, object>
        {
            { "RequestId", RequestId },
            { "RequestedAt", RequestedAt },
            { "SeverityLevel", SeverityLevel },
            { "Language", Language ?? "unknown" },
            { "CodeLength", ProblematicCode.Length }
        };
    }
}

public class CodeCorrectionRequestValidator : AbstractValidator<CodeCorrectionRequest>
{
    public CodeCorrectionRequestValidator()
    {
        RuleFor(x => x.ProblematicCode)
            .NotEmpty()
            .WithMessage("El código problemático es requerido")
            .MinimumLength(1)
            .WithMessage("El código debe tener al menos 1 carácter")
            .MaximumLength(50000)
            .WithMessage("El código no puede exceder 50000 caracteres");

        RuleFor(x => x.ErrorDescription)
            .NotEmpty()
            .WithMessage("La descripción del error es requerida")
            .MinimumLength(5)
            .WithMessage("La descripción debe tener al menos 5 caracteres")
            .MaximumLength(2000)
            .WithMessage("La descripción no puede exceder 2000 caracteres");

        RuleFor(x => x.SeverityLevel)
            .InclusiveBetween(0, 2)
            .WithMessage("El nivel de severidad debe estar entre 0 y 2");

        RuleFor(x => x.Language)
            .MaximumLength(50)
            .WithMessage("El lenguaje no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.Framework)
            .MaximumLength(50)
            .WithMessage("El framework no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Framework));
    }
}

public class CodeCorrectionRequestValidationException : Exception
{
    public IEnumerable<string> ValidationErrors { get; }
    
    public CodeCorrectionRequestValidationException(string message, IEnumerable<string> errors) 
        : base(message)
    {
        ValidationErrors = errors;
    }
}

// === ARCHIVO: src/Application/DTOs/CodeCorrectionResponse.cs ===
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CodeCorrectionResponse
{
    public string CorrectedCode { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public List<string> AppliedFixes { get; set; } = new();
    public double ConfidenceScore { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = string.Empty;
    public TimeSpan ProcessingDuration { get; set; }
    public List<CorrectionImpact> ImpactAnalysis { get; set; } = new();
    public Dictionary<string, string> Metadata { get; set; } = new();

    public CodeCorrectionResponse() { }

    public CodeCorrectionResponse(
        string correctedCode, 
        string explanation, 
        IEnumerable<string> appliedFixes, 
        double confidenceScore)
    {
        CorrectedCode = correctedCode ?? throw new ArgumentNullException(nameof(correctedCode));
        Explanation = explanation ?? string.Empty;
        AppliedFixes = appliedFixes?.ToList() ?? new List<string>();
        ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
        IsSuccess = !string.IsNullOrWhiteSpace(correctedCode);
        ProcessedAt = DateTime.UtcNow;
    }

    public bool RequiresManualReview()
    {
        return ConfidenceScore < 0.7 || IsSuccess == false || 
               AppliedFixes.Count == 0 || ImpactAnalysis.Any(i => i.BreaksExistingTests);
    }

    public string GetSummary()
    {
        var status = IsSuccess ? "Éxito" : "Fallido";
        var confidence = $"{(ConfidenceScore * 100):F1}%";
        var fixes = $"{AppliedFixes.Count} corrección(es) aplicada(s)";
        return $"{status} - Confianza: {confidence} - {fixes}";
    }

    public bool HasHighConfidence() => ConfidenceScore >= 0.8;
    
    public bool HasAppliedFixes() => AppliedFixes != null && AppliedFixes.Count > 0;
    
    public IEnumerable<string> GetCriticalFixes()
    {
        return ImpactAnalysis
            .Where(i => i.Severity == ImpactSeverity.Critical)
            .Select(i => i.Description);
    }

    public CodeCorrectionResponse WithRequestId(string requestId)
    {
        RequestId = requestId;
        return this;
    }

    public CodeCorrectionResponse WithProcessingDuration(TimeSpan duration)
    {
        ProcessingDuration = duration;
        return this;
    }
}

public class CorrectionImpact
{
    public string Description { get; set; } = string.Empty;
    public ImpactSeverity Severity { get; set; }
    public bool BreaksExistingTests { get; set; }
    public string? AffectedComponent { get; set; }
    public List<string> RelatedFiles { get; set; } = new();

    public CorrectionImpact() { }

    public CorrectionImpact(string description, ImpactSeverity severity, bool breaksTests = false)
    {
        Description = description;
        Severity = severity;
        BreaksExistingTests = breaksTests;
    }
}

public enum ImpactSeverity
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

// === ARCHIVO: src/Application/DTOs/DocumentationRequest.cs ===
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.DTOs;

public class DocumentationRequest
{
    public string CodeSnippet { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int ScopeLines { get; set; } = 10;
    public DocumentationFormat Format { get; set; } = DocumentationFormat.Markdown;
    public string? Language { get; set; }
    public bool IncludeExamples { get; set; } = true;
    public bool IncludeApiReference { get; set; } = false;
    public string? TargetAudience { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = Guid.NewGuid().ToString();

    public DocumentationRequest() { }

    public DocumentationRequest(string codeSnippet, string comment, int scopeLines = 10)
    {
        if (string.IsNullOrWhiteSpace(codeSnippet))
            throw new ArgumentException("El fragmento de código no puede estar vacío", nameof(codeSnippet));

        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("El comentario no puede estar vacío", nameof(comment));

        CodeSnippet = codeSnippet;
        Comment = comment;
        ScopeLines = scopeLines;
    }

    public bool IsValidScope() => ScopeLines > 0 && ScopeLines <= 500;
    
    public int GetEstimatedProcessingTime() => Math.Max(1, (CodeSnippet.Length / 500) + (ScopeLines / 20));
    
    public IDictionary<string, object> ToMetadata()
    {
        return new Dictionary<string, object>
        {
            { "RequestId", RequestId },
            { "RequestedAt", RequestedAt },
            { "ScopeLines", ScopeLines },
            { "Format", Format },
            { "Language", Language ?? "auto-detect" },
            { "IncludeExamples", IncludeExamples },
            { "CodeLength", CodeSnippet.Length }
        };
    }

    public string GetDocumentationContext()
    {
        var contextParts = new List<string>();
        
        if (!string.IsNullOrEmpty(Language))
            contextParts.Add($"Lenguaje: {Language}");
        
        contextParts.Add($"Líneas de alcance: {ScopeLines}");
        contextParts.Add($"Formato: {Format}");
        
        if (IncludeExamples)
            contextParts.Add("Incluir ejemplos: Sí");
            
        return string.Join(" | ", contextParts);
    }
}

public class DocumentationRequestValidator : AbstractValidator<DocumentationRequest>
{
    public DocumentationRequestValidator()
    {
        RuleFor(x => x.CodeSnippet)
            .NotEmpty()
            .WithMessage("El fragmento de código es requerido")
            .MinimumLength(1)
            .WithMessage("El código debe tener al menos 1 carácter")
            .MaximumLength(100000)
            .WithMessage("El código no puede exceder 100000 caracteres");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage("El comentario es requerido")
            .MinimumLength(3)
            .WithMessage("El comentario debe tener al menos 3 caracteres")
            .MaximumLength(5000)
            .WithMessage("El comentario no puede exceder 5000 caracteres");

        RuleFor(x => x.ScopeLines)
            .InclusiveBetween(1, 500)
            .WithMessage("Las líneas de alcance deben estar entre 1 y 500");

        RuleFor(x => x.Language)
            .MaximumLength(50)
            .WithMessage("El lenguaje no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.TargetAudience)
            .MaximumLength(100)
            .WithMessage("La audiencia objetivo no puede exceder 100 caracteres")
            .When(x => !string.IsNullOrEmpty(x.TargetAudience));

        RuleFor(x => x.Format)
            .IsInEnum()
            .WithMessage("El formato de documentación debe ser válido");
    }
}

public enum DocumentationFormat
{
    Markdown = 0,
    Html = 1,
    PlainText = 2,
    OpenApi = 3,
    XmlDoc = 4,
    Json = 5
}

public class DocumentationRequestValidationException : Exception
{
    public IEnumerable<string> ValidationErrors { get; }
    
    public DocumentationRequestValidationException(string message, IEnumerable<string> errors) 
        : base(message)
    {
        ValidationErrors = errors;
    }
}


// === ARCHIVO: src/Application/DTOs/DocumentationResponse.cs ===
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

// === ARCHIVO: src/Application/Services/CodeSuggestionService.cs ===
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CodeSuggestionService : ICodeSuggestionService
{
    private readonly ILogger<CodeSuggestionService> _logger;
    private readonly ICodeSuggestionService _infrastructureService;
    private readonly List<SuggestionMetrics> _metricsCache = new();
    private readonly object _metricsLock = new();
    private const int MaxSuggestionsPerRequest = 10;
    private const double MinConfidenceThreshold = 0.3;

    public CodeSuggestionService(
        ILogger<CodeSuggestionService> logger,
        ICodeSuggestionService infrastructureService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _infrastructureService = infrastructureService ?? throw new ArgumentNullException(nameof(infrastructureService));
    }

    public async Task<CodeSuggestionResponse> GenerateSuggestionsAsync(
        CodeSuggestionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.CurrentCode))
            throw new CodeSuggestionException("El código actual no puede estar vacío");

        _logger.LogInformation("Generando sugerencias de código para contexto: {ContextDescription}", 
            request.ContextDescription);

        try
        {
            var startTime = DateTime.UtcNow;
            var response = await _infrastructureService.GenerateSuggestionsAsync(request, cancellationToken);
            var duration = DateTime.UtcNow - startTime;

            RecordMetrics(request, response, duration);

            if (!response.HasValidSuggestions())
            {
                _logger.LogWarning("No se generaron sugerencias válidas para el código proporcionado");
            }
            else
            {
                _logger.LogInformation("Se generaron {Count} sugerencias en {Duration}ms",
                    response.Suggestions.Count(), duration.TotalMilliseconds);
            }

            return response;
        }
        catch (CodeSuggestionException ex)
        {
            _logger.LogError(ex, "Error al generar sugerencias de código");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al generar sugerencias");
            throw new CodeSuggestionException("Error al procesar la solicitud de sugerencias", ex);
        }
    }

    public IDictionary<CodeSuggestion, bool> ValidateSuggestions(IEnumerable<CodeSuggestion> suggestions)
    {
        if (suggestions == null)
            throw new ArgumentNullException(nameof(suggestions));

        var validationResults = new Dictionary<CodeSuggestion, bool>();
        var suggestionList = suggestions.ToList();

        _logger.LogDebug("Validando {Count} sugerencias de código", suggestionList.Count);

        foreach (var suggestion in suggestionList)
        {
            var isValid = ValidateSingleSuggestion(suggestion);
            validationResults[suggestion] = isValid;

            _logger.LogTrace("Sugerencia {Id} validada: {IsValid}", suggestion.Id, isValid);
        }

        var validCount = validationResults.Count(v => v.Value);
        _logger.LogInformation("Validación completada: {ValidCount}/{Total} sugerencias válidas",
            validCount, suggestionList.Count);

        return validationResults;
    }

    public async Task<IEnumerable<SuggestionMetrics>> GetPerformanceMetricsAsync(
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Obteniendo métricas de rendimiento de sugerencias");

        await Task.Delay(1, cancellationToken);

        lock (_metricsLock)
        {
            return _metricsCache.ToList().AsReadOnly();
        }
    }

    private bool ValidateSingleSuggestion(CodeSuggestion suggestion)
    {
        if (suggestion == null)
            return false;

        if (string.IsNullOrWhiteSpace(suggestion.SuggestedCode))
            return false;

        if (suggestion.SuggestedCode.Length > 10000)
            return false;

        if (suggestion.ConfidenceScore < MinConfidenceThreshold)
            return false;

        if (string.IsNullOrWhiteSpace(suggestion.Description))
            return false;

        return true;
    }

    private void RecordMetrics(CodeSuggestionRequest request, CodeSuggestionResponse response, TimeSpan duration)
    {
        var metrics = new SuggestionMetrics
        {
            RequestId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow,
            ContextDescription = request.ContextDescription,
            SuggestionsCount = response.Suggestions.Count(),
            DurationMilliseconds = duration.TotalMilliseconds,
            Success = response.HasValidSuggestions(),
            AverageConfidence = response.Suggestions.Any() 
                ? response.Suggestions.Average(s => s.ConfidenceScore) 
                : 0.0
        };

        lock (_metricsLock)
        {
            _metricsCache.Add(metrics);

            if (_metricsCache.Count > 1000)
            {
                _metricsCache.RemoveRange(0, _metricsCache.Count - 1000);
            }
        }

        _logger.LogDebug("Métricas registradas: RequestId={RequestId}, Duration={Duration}ms",
            metrics.RequestId, metrics.DurationMilliseconds);
    }

    public async Task<IEnumerable<CodeSuggestion>> FilterHighConfidenceSuggestionsAsync(
        IEnumerable<CodeSuggestion> suggestions,
        double minimumConfidence = 0.7,
        CancellationToken cancellationToken = default)
    {
        if (suggestions == null)
            return Enumerable.Empty<CodeSuggestion>();

        await Task.Delay(1, cancellationToken);

        return suggestions
            .Where(s => s.ConfidenceScore >= minimumConfidence)
            .OrderByDescending(s => s.ConfidenceScore)
            .ToList();
    }

    public async Task<CodeSuggestionResponse> GenerateSuggestionsWithRetryAsync(
        CodeSuggestionRequest request,
        int maxRetries = 3,
        CancellationToken cancellationToken = default)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                _logger.LogInformation("Intento {Attempt} de {MaxRetries} para generar sugerencias",
                    attempt, maxRetries);

                return await GenerateSuggestionsAsync(request, cancellationToken);
            }
            catch (CodeSuggestionException ex) when (attempt < maxRetries)
            {
                _logger.LogWarning(ex, "Reintentando después de error en intento {Attempt}", attempt);
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), cancellationToken);
            }
        }

        throw new CodeSuggestionException("No se pudieron generar sugerencias después de múltiples intentos");
    }
}

// === ARCHIVO: src/Application/Services/CodeCorrectionService.cs ===
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CodeCorrectionService : ICodeCorrectionService
{
    private readonly ILogger<CodeCorrectionService> _logger;
    private readonly ICodeCorrectionService _infrastructureService;
    private readonly Dictionary<string, List<CodeIssue>> _analysisCache = new();
    private readonly object _cacheLock = new();
    private const int MaxCacheSize = 500;
    private const double HighSeverityThreshold = 1.5;

    public CodeCorrectionService(
        ILogger<CodeCorrectionService> logger,
        ICodeCorrectionService infrastructureService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _infrastructureService = infrastructureService ?? throw new ArgumentNullException(nameof(infrastructureService));
    }

    public async Task<CodeCorrectionResponse> CorrectCodeAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.ProblematicCode))
            throw new CodeCorrectionException("El código problemático no puede estar vacío");

        _logger.LogInformation("Iniciando corrección de código con severidad: {SeverityLevel}",
            request.SeverityLevel);

        try
        {
            var startTime = DateTime.UtcNow;
            var response = await _infrastructureService.CorrectCodeAsync(request, cancellationToken);
            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation("Corrección completada en {Duration}ms. Éxito: {IsSuccess}",
                duration.TotalMilliseconds, response.IsSuccess);

            if (response.RequiresManualReview())
            {
                _logger.LogWarning("La corrección requiere revisión manual. Puntuación de confianza: {Confidence}",
                    response.ConfidenceScore);
            }

            return response;
        }
        catch (CodeCorrectionException ex)
        {
            _logger.LogError(ex, "Error al corregir el código");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante la corrección");
            throw new CodeCorrectionException("Error al procesar la solicitud de corrección", ex);
        }
    }

    public async Task<IEnumerable<CodeIssue>> AnalyzeCodeAsync(
        string code,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("El código a analizar no puede estar vacío", nameof(code));

        var cacheKey = GenerateCacheKey(code);

        lock (_cacheLock)
        {
            if (_analysisCache.TryGetValue(cacheKey, out var cachedIssues))
            {
                _logger.LogDebug("Usando análisis en caché para clave: {CacheKey}", cacheKey);
                return cachedIssues;
            }
        }

        _logger.LogDebug("Analizando código de longitud: {Length}", code.Length);

        try
        {
            var issues = await _infrastructureService.AnalyzeCodeAsync(code, cancellationToken);
            var issueList = issues.ToList();

            lock (_cacheLock)
            {
                _analysisCache[cacheKey] = issueList;

                if (_analysisCache.Count > MaxCacheSize)
                {
                    var keysToRemove = _analysisCache.Keys.Take(_analysisCache.Count - MaxCacheSize).ToList();
                    foreach (var key in keysToRemove)
                    {
                        _analysisCache.Remove(key);
                    }
                }
            }

            var criticalCount = issueList.Count(i => i.IsCritical());
            _logger.LogInformation("Análisis completado: {Total} problemas encontrados, {Critical} críticos",
                issueList.Count, criticalCount);

            return issueList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al analizar el código");
            throw new CodeCorrectionException("Error durante el análisis de código", ex);
        }
    }

    public async Task<int> EstimateCorrectionComplexityAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogDebug("Estimando complejidad de corrección para código de longitud: {Length}",
            request.ProblematicCode?.Length ?? 0);

        try
        {
            var complexity = await _infrastructureService.EstimateCorrectionComplexityAsync(request, cancellationToken);

            _logger.LogInformation("Complejidad estimada: {Complexity}", complexity);

            return complexity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al estimar la complejidad");
            return CalculateLocalComplexity(request);
        }
    }

    private string GenerateCacheKey(string code)
    {
        var hash = code.GetHashCode();
        var length = code.Length;
        return $"{length}_{hash}";
    }

    private int CalculateLocalComplexity(CodeCorrectionRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.ProblematicCode))
            return 0;

        var code = request.ProblematicCode;
        int complexity = 1;

        complexity += code.Count(c => c == '{') + code.Count(c => c == '}');
        complexity += code.Split('\n').Length / 10;
        complexity += request.SeverityLevel;

        if (code.Contains("async") || code.Contains("await"))
            complexity += 2;

        if (code.Contains("foreach") || code.Contains("for") || code.Contains("while"))
            complexity += code.Split(new[] { "foreach", "for", "while" }, StringSplitOptions.None).Length - 1;

        return Math.Min(complexity, 10);
    }

    public async Task<CodeCorrectionResponse> CorrectCodeWithFallbackAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await CorrectCodeAsync(request, cancellationToken);
        }
        catch (CodeCorrectionException ex)
        {
            _logger.LogWarning(ex, "Corrección primaria falló, generando respuesta de fallback");

            return new CodeCorrectionResponse(
                request.ProblematicCode,
                "No se pudo completar la corrección automática. Se recomienda revisión manual.",
                Enumerable.Empty<string>(),
                0.0
            );
        }
    }

    public async Task<IEnumerable<CodeIssue>> AnalyzeCodeWithDepthAsync(
        string code,
        int maxDepth = 3,
        CancellationToken cancellationToken = default)
    {
        if (maxDepth <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxDepth), "La profundidad debe ser mayor que 0");

        var allIssues = new List<CodeIssue>();

        for (int depth = 1; depth <= maxDepth; depth++)
        {
            _logger.LogDebug("Ejecutando análisis en nivel de profundidad: {Depth}", depth);
            var issues = await AnalyzeCodeAsync(code, cancellationToken);
            allIssues.AddRange(issues);
        }

        return allIssues.Distinct().ToList();
    }

    public void ClearCache()
    {
        lock (_cacheLock)
        {
            var count = _analysisCache.Count;
            _analysisCache.Clear();
            _logger.LogInformation("Caché de análisis limpiada. Elementos eliminados: {Count}", count);
        }
    }
}

// === ARCHIVO: src/Application/Services/DocumentationGeneratorService.cs ===
namespace Application.Services;

using Domain.Interfaces;
using Domain.Models;
using Application.DTOs;

public class DocumentationGeneratorService : IDocumentationGeneratorService
{
    private readonly ICodeSuggestionService _suggestionService;
    private readonly ICodeCorrectionService _correctionService;
    private readonly ILogger<DocumentationGeneratorService> _logger;
    private const int MaxDocumentationLength = 10000;
    private const int DefaultContextLines = 10;

    public DocumentationGeneratorService(
        ICodeSuggestionService suggestionService,
        ICodeCorrectionService correctionService,
        ILogger<DocumentationGeneratorService> logger)
    {
        _suggestionService = suggestionService ?? throw new ArgumentNullException(nameof(suggestionService));
        _correctionService = correctionService ?? throw new ArgumentNullException(nameof(correctionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DocumentationResponse> GenerateDocumentationAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new DocumentationGenerationException("La solicitud de documentación no puede ser nula.");
        }

        _logger.LogInformation("Iniciando generación de documentación para snippet de {CodeLength} caracteres",
            request.CodeSnippet?.Length ?? 0);

        try
        {
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validación de solicitud falló: {Errors}", string.Join(", ", validationResult.Errors));
                throw new DocumentationGenerationException($"Validación fallida: {string.Join(", ", validationResult.Errors)}");
            }

            var documentationContent = await GenerateDocumentationContentAsync(request, cancellationToken);
            
            var response = new DocumentationResponse
            {
                Documentation = documentationContent.GeneratedContent,
                SourceCode = request.CodeSnippet,
                Comment = request.Comment,
                ScopeLines = request.ScopeLines,
                ConfidenceScore = documentationContent.ConfidenceScore,
                GeneratedAt = DateTime.UtcNow,
                Metadata = new Dictionary<string, object>
                {
                    { "processingTimeMs", documentationContent.ProcessingTime.TotalMilliseconds },
                    { "sectionsGenerated", documentationContent.Sections.Count },
                    { "originalCodeLength", request.CodeSnippet.Length }
                }
            };

            _logger.LogInformation("Documentación generada exitosamente con confianza {Confidence}",
                response.ConfidenceScore);

            return response;
        }
        catch (DocumentationGenerationException)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Generación de documentación cancelada");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al generar documentación");
            throw new DocumentationGenerationException("Error al generar documentación", ex);
        }
    }

    public async Task<IEnumerable<DocumentationSection>> GetDocumentationSectionsAsync(
        string codeSnippet,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(codeSnippet))
        {
            return Enumerable.Empty<DocumentationSection>();
        }

        var sections = new List<DocumentationSection>();
        var lines = codeSnippet.Split('\n');
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (IsDocumentableLine(line))
            {
                sections.Add(new DocumentationSection
                {
                    LineNumber = i + 1,
                    Content = line,
                    SectionType = DetermineSectionType(line),
                    Description = await AnalyzeLineForDocumentationAsync(line, cancellationToken)
                });
            }
        }

        return sections;
    }

    public async Task<DocumentationMetrics> GetDocumentationMetricsAsync(
        DocumentationResponse response,
        CancellationToken cancellationToken = default)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        var metrics = new DocumentationMetrics
        {
            TotalCharacters = response.Documentation?.Length ?? 0,
            TotalWords = CountWords(response.Documentation ?? string.Empty),
            TotalLines = CountLines(response.Documentation ?? string.Empty),
            ConfidenceScore = response.ConfidenceScore,
            GeneratedAt = response.GeneratedAt,
            ProcessingTimeMs = response.Metadata?.ContainsKey("processingTimeMs") == true
                ? Convert.ToDouble(response.Metadata["processingTimeMs"])
                : 0
        };

        metrics.CompletenessScore = CalculateCompletenessScore(metrics);
        metrics.QualityScore = await CalculateQualityScoreAsync(response, cancellationToken);

        return metrics;
    }

    private ValidationResult ValidateRequest(DocumentationRequest request)
    {
        var errors = new List<string>();

        if (request == null)
        {
            errors.Add("La solicitud no puede ser nula");
            return new ValidationResult { IsValid = false, Errors = errors };
        }

        if (string.IsNullOrWhiteSpace(request.CodeSnippet))
        {
            errors.Add("El código no puede estar vacío");
        }
        else if (request.CodeSnippet.Length > MaxDocumentationLength)
        {
            errors.Add($"El código excede el límite de {MaxDocumentationLength} caracteres");
        }

        if (request.ScopeLines < 1 || request.ScopeLines > 100)
        {
            errors.Add("El rango de líneas debe estar entre 1 y 100");
        }

        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }

    private async Task<DocumentationContentResult> GenerateDocumentationContentAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;
        var sections = new List<string>();
        
        var codeAnalysis = await AnalyzeCodeStructureAsync(request.CodeSnippet, cancellationToken);
        sections.Add(GenerateSummarySection(request.CodeSnippet, request.Comment));
        
        var parameterDocs = await GenerateParameterDocumentationAsync(request.CodeSnippet, cancellationToken);
        if (!string.IsNullOrEmpty(parameterDocs))
        {
            sections.Add(parameterDocs);
        }

        var returnDocs = await GenerateReturnDocumentationAsync(request.CodeSnippet, cancellationToken);
        if (!string.IsNullOrEmpty(returnDocs))
        {
            sections.Add(returnDocs);
        }

        var usageExample = GenerateUsageExample(request.CodeSnippet);
        sections.Add(usageExample);

        var processingTime = DateTime.UtcNow - startTime;
        var confidenceScore = CalculateConfidenceScore(codeAnalysis, sections.Count);

        return new DocumentationContentResult
        {
            GeneratedContent = string.Join("\n\n", sections),
            Sections = sections,
            ConfidenceScore = confidenceScore,
            ProcessingTime = processingTime
        };
    }

    private async Task<CodeAnalysisResult> AnalyzeCodeStructureAsync(
        string codeSnippet,
        CancellationToken cancellationToken)
    {
        var result = new CodeAnalysisResult
        {
            HasMethods = codeSnippet.Contains("public") || codeSnippet.Contains("private"),
            HasClasses = codeSnippet.Contains("class"),
            HasInterfaces = codeSnippet.Contains("interface"),
            HasParameters = codeSnippet.Contains("(") && codeSnippet.Contains(")"),
            HasReturnType = codeSnippet.Contains("return") || codeSnippet.Contains(": "),
            LineCount = codeSnippet.Split('\n').Length
        };

        await Task.CompletedTask;
        return result;
    }

    private string GenerateSummarySection(string codeSnippet, string comment)
    {
        var summaryBuilder = new System.Text.StringBuilder();
        summaryBuilder.AppendLine("## Resumen");
        
        if (!string.IsNullOrWhiteSpace(comment))
        {
            summaryBuilder.AppendLine($"Descripción basada en el comentario proporcionado: {comment}");
        }
        else
        {
            var codeType = DetectCodeType(codeSnippet);
            summaryBuilder.AppendLine($"Este código implementa un componente de tipo: {codeType}");
        }

        return summaryBuilder.ToString();
    }

    private async Task<string> GenerateParameterDocumentationAsync(
        string codeSnippet,
        CancellationToken cancellationToken)
    {
        var paramRegex = new System.Text.RegularExpressions.Regex(@"\((.*?)\)");
        var match = paramRegex.Match(codeSnippet);

        if (!match.Success)
        {
            return string.Empty;
        }

        var parameters = match.Groups[1].Value.Split(',')
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrEmpty(p) && !p.Contains("="))
            .ToList();

        if (parameters.Count == 0)
        {
            return string.Empty;
        }

        var docBuilder = new System.Text.StringBuilder();
        docBuilder.AppendLine("## Parámetros");

        foreach (var param in parameters)
        {
            var paramName = ExtractParameterName(param);
            var paramType = ExtractParameterType(param);
            docBuilder.AppendLine($"- **{paramName}** ({paramType}): Parámetro de entrada del método");
        }

        await Task.CompletedTask;
        return docBuilder.ToString();
    }

    private async Task<string> GenerateReturnDocumentationAsync(
        string codeSnippet,
        CancellationToken cancellationToken)
    {
        var returnTypes = new[] { "Task<", "void", "string", "int", "bool", "object", "IEnumerable<", "IActionResult" };
        var detectedReturnType = returnTypes.FirstOrDefault(rt => codeSnippet.Contains(rt));

        if (string.IsNullOrEmpty(detectedReturnType))
        {
            return string.Empty;
        }

        var docBuilder = new System.Text.StringBuilder();
        docBuilder.AppendLine("## Retorno");
        docBuilder.AppendLine($"- **Tipo**: {detectedReturnType}");
        docBuilder.AppendLine("- **Descripción**: Valor retornado por la operación");

        await Task.CompletedTask;
        return docBuilder.ToString();
    }

    private string GenerateUsageExample(string codeSnippet)
    {
        var exampleBuilder = new System.Text.StringBuilder();
        exampleBuilder.AppendLine("## Ejemplo de Uso");
        
        var methodName = ExtractMethodName(codeSnippet);
        if (!string.IsNullOrEmpty(methodName))
        {
            exampleBuilder.AppendLine("```csharp");
            exampleBuilder.AppendLine($"// Llamada al método");
            exampleBuilder.AppendLine($"var result = {methodName}(/* parámetros */);");
            exampleBuilder.AppendLine("```");
        }

        return exampleBuilder.ToString();
    }

    private string DetectCodeType(string codeSnippet)
    {
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Controller"))
            return "Controlador MVC";
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Service"))
            return "Servicio de aplicación";
        if (codeSnippet.Contains("interface ") || codeSnippet.Contains("abstract "))
            return "Contrato/Interfaz";
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Entity"))
            return "Entidad de dominio";
        if (codeSnippet.Contains("class ") && codeSnippet.Contains("Repository"))
            return "Repositorio";
        if (codeSnippet.Contains("class "))
            return "Clase";
        
        return "Código";
    }

    private string ExtractMethodName(string codeSnippet)
    {
        var methodRegex = new System.Text.RegularExpressions.Regex(@"(?:public|private|protected)\s+(?:async\s+)?(?:\w+(?:<[^>]+>)?)\s+(\w+)\s*\(");
        var match = methodRegex.Match(codeSnippet);
        return match.Success ? match.Groups[1].Value : "methodName";
    }

    private string ExtractParameterName(string parameter)
    {
        var parts = parameter.Split(' ');
        return parts.Length > 1 ? parts[^1] : parameter;
    }

    private string ExtractParameterType(string parameter)
    {
        var parts = parameter.Split(' ');
        return parts.Length > 0 ? parts[0] : "object";
    }

    private bool IsDocumentableLine(string line)
    {
        var documentableKeywords = new[] { "public", "private", "protected", "class", "interface", "void", "Task", "string", "int", "bool" };
        return documentableKeywords.Any(kw => line.Contains(kw));
    }

    private DocumentationSectionType DetermineSectionType(string line)
    {
        if (line.Contains("class ") || line.Contains("interface "))
            return DocumentationSectionType.TypeDeclaration;
        if (line.Contains("public ") || line.Contains("private "))
            return DocumentationSectionType.Member;
        if (line.Contains("//") || line.Contains("/*"))
            return DocumentationSectionType.Comment;
        
        return DocumentationSectionType.Other;
    }

    private async Task<string> AnalyzeLineForDocumentationAsync(string line, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (line.Contains("class "))
            return "Definición de clase";
        if (line.Contains("interface "))
            return "Definición de interfaz";
        if (line.Contains("public ") && line.Contains("("))
            return "Método público";
        
        return "Línea de código";
    }

    private double CalculateConfidenceScore(CodeAnalysisResult analysis, int sectionsCount)
    {
        var baseScore = 0.5;
        
        if (analysis.HasMethods) baseScore += 0.15;
        if (analysis.HasClasses) baseScore += 0.1;
        if (analysis.HasParameters) baseScore += 0.15;
        if (analysis.HasReturnType) baseScore += 0.1;
        
        return Math.Min(1.0, baseScore);
    }

    private int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    private int CountLines(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        return text.Split('\n').Length;
    }

    private double CalculateCompletenessScore(DocumentationMetrics metrics)
    {
        var score = 0.0;
        
        if (metrics.TotalCharacters > 100) score += 0.3;
        if (metrics.TotalWords > 20) score += 0.3;
        if (metrics.TotalLines > 5) score += 0.2;
        if (metrics.ConfidenceScore > 0.7) score += 0.2;
        
        return score;
    }

    private async Task<double> CalculateQualityScoreAsync(
        DocumentationResponse response,
        CancellationToken cancellationToken)
    {
        var qualityScore = 0.5;
        
        if (!string.IsNullOrWhiteSpace(response.Documentation))
        {
            qualityScore += 0.2;
            
            if (response.Documentation.Contains("##"))
            {
                qualityScore += 0.15;
            }
            
            if (response.Documentation.Contains("```"))
            {
                qualityScore += 0.15;
            }
        }

        await Task.CompletedTask;
        return Math.Min(1.0, qualityScore);
    }

    private class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    private class DocumentationContentResult
    {
        public string GeneratedContent { get; set; } = string.Empty;
        public List<string> Sections { get; set; } = new();
        public double ConfidenceScore { get; set; }
        public TimeSpan ProcessingTime { get; set; }
    }

    private class CodeAnalysisResult
    {
        public bool HasMethods { get; set; }
        public bool HasClasses { get; set; }
        public bool HasInterfaces { get; set; }
        public bool HasParameters { get; set; }
        public bool HasReturnType { get; set; }
        public int LineCount { get; set; }
    }
}

public enum DocumentationSectionType
{
    TypeDeclaration,
    Member,
    Comment,
    Other
}

public class DocumentationSection
{
    public int LineNumber { get; set; }
    public string Content { get; set; } = string.Empty;
    public DocumentationSectionType SectionType { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class DocumentationMetrics
{
    public int TotalCharacters { get; set; }
    public int TotalWords { get; set; }
    public int TotalLines { get; set; }
    public double ConfidenceScore { get; set; }
    public double CompletenessScore { get; set; }
    public double QualityScore { get; set; }
    public DateTime GeneratedAt { get; set; }
    public double ProcessingTimeMs { get; set; }
}

// === ARCHIVO: src/Infrastructure/Infrastructure.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="AWSSDK.CodeWhisperer" Version="3.7.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Options" Version="9.0.0" />
    <PackageReference Include="System.Text.Json" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Domain\Domain.csproj" />
    <ProjectReference Include="..\Application\Application.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Services" />
    <Folder Include="Extensions" />
    <Folder Include="Configuration" />
    <Folder Include="Adapters" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/Infrastructure/Services/AwsCodeWhispererService.cs ===
using Amazon.CodeWhisperer;
using Amazon.CodeWhisperer.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Services;

public class AwsCodeWhispererService : ICodeSuggestionService, ICodeCorrectionService, IDocumentationGeneratorService
{
    private readonly AmazonCodeWhispererClient _whispererClient;
    private readonly ILogger<AwsCodeWhispererService> _logger;
    private readonly AwsCodeWhispererOptions _options;
    private readonly HttpClient _httpClient;

    public AwsCodeWhispererService(
        IOptions<AwsCodeWhispererOptions> options,
        ILogger<AwsCodeWhispererService> logger)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        var credentials = GetCredentials();
        var config = new AmazonCodeWhispererConfig
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_options.Region),
            Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds)
        };
        
        _whispererClient = new AmazonCodeWhispererClient(credentials, config);
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
    }

    private AWSCredentials GetCredentials()
    {
        var chain = new CredentialProfileStoreChain();
        if (chain.TryGetAWSCredentials(_options.ProfileName, out var credentials))
        {
            return credentials!;
        }
        
        if (!string.IsNullOrEmpty(_options.AccessKeyId) && !string.IsNullOrEmpty(_options.SecretAccessKey))
        {
            return new BasicAWSCredentials(_options.AccessKeyId, _options.SecretAccessKey);
        }
        
        return new AnonymousAWSCredentials();
    }

    public async Task<CodeSuggestionResponse> GenerateSuggestionsAsync(
        CodeSuggestionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Generando sugerencias de código para contexto: {Context}", 
            request.ContextDescription);

        try
        {
            var suggestions = new List<CodeSuggestion>();
            var startTime = DateTime.UtcNow;

            var generateSuggestionsRequest = new GenerateSuggestionsRequest
            {
                FileContext = request.CurrentCode,
                MaxSuggestions = request.SuggestionsCount,
                CompletionType = CompletionType.Auto,
                FeatureContext = request.ContextDescription
            };

            var response = await _whispererClient.GenerateSuggestionsAsync(generateSuggestionsRequest, cancellationToken);

            if (response.Suggestions != null && response.Suggestions.Any())
            {
                var index = 0;
                foreach (var suggestion in response.Suggestions)
                {
                    suggestions.Add(new CodeSuggestion(
                        suggestion.Content,
                        suggestion.Confidence ?? 0.85,
                        GetSuggestionType(suggestion.SuggestionType),
                        $"Suggestion-{Guid.NewGuid():N}"
                    ));
                    index++;
                }
            }
            else
            {
                suggestions.AddRange(GenerateFallbackSuggestions(request));
            }

            var duration = DateTime.UtcNow - startTime;
            _logger.LogInformation("Generadas {Count} sugerencias en {Duration}ms", 
                suggestions.Count, duration.TotalMilliseconds);

            return new CodeSuggestionResponse(suggestions, request.ContextDescription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar sugerencias de código");
            return new CodeSuggestionResponse(GenerateFallbackSuggestions(request), request.ContextDescription);
        }
    }

    private IEnumerable<CodeSuggestion> GenerateFallbackSuggestions(CodeSuggestionRequest request)
    {
        var suggestions = new List<CodeSuggestion>();
        var codeLines = request.CurrentCode.Split('\n');
        var lastLine = codeLines.Length > 0 ? codeLines.Last().Trim() : string.Empty;

        if (!lastLine.Contains("{") && !lastLine.Contains("}"))
        {
            suggestions.Add(new CodeSuggestion(
                "// Considera agregar documentación XML para el método",
                0.75,
                "documentation",
                $"Fallback-{Guid.NewGuid():N}"
            ));
        }

        if (request.CurrentCode.Contains("foreach") && !request.CurrentCode.Contains("null"))
        {
            suggestions.Add(new CodeSuggestion(
                "// Considera verificar null antes de iterar",
                0.80,
                "best-practice",
                $"Fallback-{Guid.NewGuid():N}"
            ));
        }

        return suggestions;
    }

    private static string GetSuggestionType(string? awsType)
    {
        return awsType?.ToLowerInvariant() switch
        {
            "completion" => "code-completion",
            "refactor" => "refactoring",
            "comment" => "documentation",
            _ => "general"
        };
    }

    public IDictionary<CodeSuggestion, bool> ValidateSuggestions(IEnumerable<CodeSuggestion> suggestions)
    {
        var results = new Dictionary<CodeSuggestion, bool>();
        
        foreach (var suggestion in suggestions)
        {
            var isValid = !string.IsNullOrWhiteSpace(suggestion.SuggestedCode) &&
                         suggestion.SuggestedCode.Length <= 10000 &&
                         suggestion.ConfidenceScore >= 0 &&
                         suggestion.ConfidenceScore <= 1;
            
            results[suggestion] = isValid;
        }
        
        _logger.LogInformation("Validadas {Count} sugerencias, {Valid} válidas",
            results.Count, results.Count(v => v.Value));
        
        return results;
    }

    public async Task<IEnumerable<SuggestionMetrics>> GetPerformanceMetricsAsync(
        CancellationToken cancellationToken = default)
    {
        var metrics = new List<SuggestionMetrics>
        {
            new SuggestionMetrics
            {
                TotalSuggestions = 0,
                AcceptedSuggestions = 0,
                RejectedSuggestions = 0,
                AverageConfidence = 0.0,
                AverageResponseTimeMs = 0,
                Timestamp = DateTime.UtcNow
            }
        };

        return await Task.FromResult(metrics);
    }

    public async Task<CodeCorrectionResponse> CorrectCodeAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Corrigiendo código con severidad: {Severity}", request.SeverityLevel);

        try
        {
            var corrections = new List<string>();
            var correctedCode = request.ProblematicCode;
            var explanation = string.Empty;
            double confidenceScore = 0.9;

            correctedCode = ApplyBasicCorrections(correctedCode, request.ErrorDescription, corrections);
            
            if (request.SeverityLevel >= 1)
            {
                correctedCode = ApplySecurityCorrections(correctedCode, corrections);
            }
            
            if (request.SeverityLevel >= 2)
            {
                correctedCode = ApplyPerformanceCorrections(correctedCode, corrections);
                confidenceScore = 0.75;
            }

            explanation = $"Se aplicaron {corrections.Count} correcciones al código. " +
                         $"El código corregido aborda los problemas identificados con nivel de severidad {request.SeverityLevel}.";

            _logger.LogInformation("Corrección completada: {Count} cambios aplicados", corrections.Count);

            return new CodeCorrectionResponse(correctedCode, explanation, corrections, confidenceScore);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al corregir código");
            throw new CodeCorrectionException("Error al procesar la corrección de código", ex);
        }
    }

    private string ApplyBasicCorrections(string code, string errorDescription, List<string> corrections)
    {
        var corrected = code;

        if (errorDescription.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            corrected = corrected.Replace("object", "object?");
            corrections.Add("Agregado null-forgiving operator para tipos potencialmente nulos");
        }

        if (errorDescription.Contains("unused", StringComparison.OrdinalIgnoreCase))
        {
            corrections.Add("Identificada variable no utilizada - considerar eliminar");
        }

        if (!corrected.Contains("namespace") && corrected.Contains("class"))
        {
            corrections.Add("Código fuera de namespace - se recomienda envolver en namespace");
        }

        return corrected;
    }

    private string ApplySecurityCorrections(string code, List<string> corrections)
    {
        var corrected = code;

        if (code.Contains("password") || code.Contains("secret") || code.Contains("apiKey"))
        {
            corrected = corrected.Replace("=\"", "=Environment.GetEnvironmentVariable(\"");
            corrections.Add("Credenciales movidas a variables de entorno");
        }

        if (code.Contains("SELECT *"))
        {
            corrected = corrected.Replace("SELECT *", "SELECT columnas_especificas");
            corrections.Add("Evitado SELECT * para prevenir exposición de datos");
        }

        return corrected;
    }

    private string ApplyPerformanceCorrections(string code, List<string> corrections)
    {
        var corrected = code;

        if (code.Contains("foreach") && !code.Contains("ToList"))
        {
            corrections.Add("Considerar usar lazy enumeration en lugar de evaluación eager");
        }

        if (code.Contains("new HttpClient()"))
        {
            corrections.Add("HttpClient debe ser inyectado como singleton para evitar agotamiento de sockets");
        }

        return corrected;
    }

    public async Task<IEnumerable<CodeIssue>> AnalyzeCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        var issues = new List<CodeIssue>();

        if (string.IsNullOrWhiteSpace(code))
        {
            return issues;
        }

        if (code.Contains("SELECT *"))
        {
            issues.Add(new CodeIssue
            {
                LineNumber = code.Split('\n').ToList().FindIndex(l => l.Contains("SELECT *")) + 1,
                Severity = 1,
                Description = "Uso de SELECT * puede causar problemas de rendimiento y exposición de datos",
                CodeSnippet = "SELECT *"
            });
        }

        if (code.Contains("new HttpClient()"))
        {
            issues.Add(new CodeIssue
            {
                LineNumber = code.Split('\n').ToList().FindIndex(l => l.Contains("new HttpClient()")) + 1,
                Severity = 2,
                Description = "Instanciación de HttpClient en método causa agotamiento de recursos",
                CodeSnippet = "new HttpClient()"
            });
        }

        if (code.Contains("catch (Exception)") || code.Contains("catch(Exception)"))
        {
            issues.Add(new CodeIssue
            {
                LineNumber = code.Split('\n').ToList().FindIndex(l => l.Contains("catch")) + 1,
                Severity = 1,
                Description = "Captura de Exception genérica oculta errores específicos",
                CodeSnippet = "catch (Exception)"
            });
        }

        return await Task.FromResult(issues);
    }

    public async Task<int> EstimateCorrectionComplexityAsync(
        CodeCorrectionRequest request,
        CancellationToken cancellationToken = default)
    {
        var complexity = 1;

        if (request.ProblematicCode.Contains("async") && !request.ProblematicCode.Contains("await"))
        {
            complexity += 2;
        }

        if (request.ProblematicCode.Length > 500)
        {
            complexity += 1;
        }

        complexity += request.SeverityLevel * 2;

        return await Task.FromResult(complexity);
    }

    public async Task<DocumentationResponse> GenerateDocumentationAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Generando documentación para snippet de {Length} caracteres", 
            request.CodeSnippet.Length);

        try
        {
            var documentation = new List<DocumentationSection>();
            var codeLines = request.CodeSnippet.Split('\n');
            var currentSection = new DocumentationSection
            {
                Title = "Resumen",
                Content = GenerateSummaryFromCode(codeLines),
                Order = 1
            };
            documentation.Add(currentSection);

            if (codeLines.Any(l => l.Contains("///") || l.Contains("/**")))
            {
                var existingDocs = ExtractExistingDocumentation(codeLines);
                documentation.Add(new DocumentationSection
                {
                    Title = "Documentación Existente",
                    Content = existingDocs,
                    Order = 2
                });
            }

            var paramDocs = ExtractParameterDocumentation(codeLines);
            if (!string.IsNullOrEmpty(paramDocs))
            {
                documentation.Add(new DocumentationSection
                {
                    Title = "Parámetros",
                    Content = paramDocs,
                    Order = 3
                });
            }

            var returnDoc = ExtractReturnDocumentation(codeLines);
            if (!string.IsNullOrEmpty(returnDoc))
            {
                documentation.Add(new DocumentationSection
                {
                    Title = "Retorno",
                    Content = returnDoc,
                    Order = 4
                });
            }

            _logger.LogInformation("Generada documentación con {Count} secciones", documentation.Count);

            return await Task.FromResult(new DocumentationResponse(
                documentation,
                request.CodeSnippet,
                true
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar documentación");
            throw new DocumentationGenerationException("Error al generar documentación", ex);
        }
    }

    private string GenerateSummaryFromCode(string[] codeLines)
    {
        var classOrMethod = codeLines.FirstOrDefault(l => 
            l.Contains("class ") || 
            l.Contains("interface ") || 
            l.Contains("method ") ||
            l.Contains("public ") ||
            l.Contains("private ") ||
            l.Contains("protected "));

        if (string.IsNullOrEmpty(classOrMethod))
        {
            return "Código sin clasificación clara. Se recomienda agregar documentación.";
        }

        if (classOrMethod.Contains("class"))
        {
            var className = ExtractName(classOrMethod, "class");
            return $"Representa la clase {className} que encapsula la lógica de negocio relacionada.";
        }

        if (classOrMethod.Contains("interface"))
        {
            var interfaceName = ExtractName(classOrMethod, "interface");
            return $"Definición del contrato {interfaceName} para la interacción con componentes externos.";
        }

        return "Método que ejecuta una operación específica dentro del contexto de la aplicación.";
    }

    private string ExtractName(string line, string keyword)
    {
        var parts = line.Split(keyword);
        if (parts.Length > 1)
        {
            var namePart = parts[1].Trim().Split(' ')[0].Split('{')[0].Trim();
            return namePart;
        }
        return "Desconocido";
    }

    private string ExtractExistingDocumentation(string[] codeLines)
    {
        var docLines = codeLines
            .Where(l => l.Trim().StartsWith("///") || l.Trim().StartsWith("/**"))
            .Select(l => l.Replace("///", "").Replace("/**", "").Replace("*/", "").Trim());

        return string.Join("\n", docLines);
    }

    private string ExtractParameterDocumentation(string[] codeLines)
    {
        var paramLines = codeLines
            .Where(l => l.Contains("@param") || l.Contains("/// <param"))
            .Select(l => l.Replace("@param", "").Replace("/// <param name=", "").Replace("</param>", "").Trim());

        return string.Join("\n", paramLines);
    }

    private string ExtractReturnDocumentation(string[] codeLines)
    {
        var returnLine = codeLines
            .FirstOrDefault(l => l.Contains("@return") || l.Contains("/// <returns"));

        if (returnLine == null)
            return string.Empty;

        return returnLine.Replace("@return", "").Replace("/// <returns>", "").Replace("</returns>", "").Trim();
    }
}

public class AwsCodeWhispererOptions
{
    public string Region { get; set; } = "us-east-1";
    public string ProfileName { get; set; } = "default";
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public int TimeoutSeconds { get; set; } = 30;
    public bool EnableAutoSuggestions { get; set; } = true;
    public int MaxTokensPerSuggestion { get; set; } = 256;
}

// === ARCHIVO: src/Infrastructure/Extensions/ServiceCollectionExtensions.cs ===
using Domain.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        services.AddOptions<AwsCodeWhispererOptions>()
            .Bind(configuration.GetSection("AWS:CodeWhisperer"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<AwsCodeWhispererOptions>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AwsCodeWhispererOptions>>();
            return options.Value;
        });

        services.AddSingleton<ICodeSuggestionService, AwsCodeWhispererService>();
        services.AddSingleton<ICodeCorrectionService, AwsCodeWhispererService>();
        services.AddSingleton<IDocumentationGeneratorService, AwsCodeWhispererService>();

        services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddConsole();
            builder.AddDebug();
        });

        services.AddHttpClient("CodeWhispererApi", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        RegisterAdditionalServices(services, configuration);

        return services;
    }

    private static void RegisterAdditionalServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<ICodeAnalysisCache, InMemoryCodeAnalysisCache>();

        var retrySettings = configuration.GetSection("AWS:RetrySettings").Get<RetrySettings>();
        if (retrySettings != null)
        {
            services.AddSingleton(retrySettings);
        }
        else
        {
            services.AddSingleton(new RetrySettings
            {
                MaxRetries = 3,
                DelayMilliseconds = 1000,
                ExponentialBackoff = true
            });
        }
    }

    public static IServiceCollection AddCodeWhispererWithResilience(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);

        services.AddSingleton<ICircuitBreakerService, CircuitBreakerService>();

        services.AddSingleton<IRetryPolicyProvider, DefaultRetryPolicyProvider>();

        return services;
    }

    public static IServiceCollection AddHealthChecksForInfrastructure(
        this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("aws-credentials", () =>
            {
                try
                {
                    return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(
                        "AWS credentials configured");
                }
                catch
                {
                    return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Unhealthy(
                        "AWS credentials not configured");
                }
            });

        return services;
    }
}

public interface ICodeAnalysisCache
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default);
    Task InvalidateAsync(string key, CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken = default);
}

public class InMemoryCodeAnalysisCache : ICodeAnalysisCache
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly SemaphoreSlim _lock = new(1, 1);

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (entry.Expiration > DateTime.UtcNow)
                {
                    return (T?)entry.Value;
                }
                _cache.Remove(key);
            }
            return default;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task SetAsync<T>(
        string key, 
        T value, 
        TimeSpan expiration, 
        CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _cache[key] = new CacheEntry
            {
                Value = value,
                Expiration = DateTime.UtcNow.Add(expiration)
            };
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task InvalidateAsync(string key, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _cache.Remove(key);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _cache.Clear();
        }
        finally
        {
            _lock.Release();
        }
    }

    private class CacheEntry
    {
        public object? Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}

public class RetrySettings
{
    public int MaxRetries { get; set; } = 3;
    public int DelayMilliseconds { get; set; } = 1000;
    public bool ExponentialBackoff { get; set; } = true;
    public int MaxDelayMilliseconds { get; set; } = 30000;
}

public interface ICircuitBreakerService
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
    Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
    CircuitBreakerState GetState();
    void Reset();
}

public enum CircuitBreakerState
{
    Closed,
    Open,
    HalfOpen
}

public class CircuitBreakerService : ICircuitBreakerService
{
    private readonly ILogger<CircuitBreakerService> _logger;
    private readonly object _lock = new();
    private CircuitBreakerState _state = CircuitBreakerState.Closed;
    private int _failureCount = 0;
    private int _successCount = 0;
    private DateTime _lastFailureTime = DateTime.MinValue;
    private const int FailureThreshold = 5;
    private const int SuccessThreshold = 2;
    private static readonly TimeSpan OpenStateDuration = TimeSpan.FromSeconds(30);

    public CircuitBreakerService(ILogger<CircuitBreakerService> logger)
    {
        _logger = logger;
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        if (_state == CircuitBreakerState.Open)
        {
            if (DateTime.UtcNow - _lastFailureTime > OpenStateDuration)
            {
                _state = CircuitBreakerState.HalfOpen;
                _logger.LogInformation("CircuitBreaker transitioned to HalfOpen state");
            }
            else
            {
                throw new CircuitBreakerOpenException("Circuit breaker is open");
            }
        }

        try
        {
            var result = await action();
            OnSuccess();
            return result;
        }
        catch (Exception ex)
        {
            OnFailure();
            _logger.LogError(ex, "CircuitBreaker execution failed");
            throw;
        }
    }

    public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(async () =>
        {
            await action();
            return true;
        }, cancellationToken);
    }

    private void OnSuccess()
    {
        lock (_lock)
        {
            _failureCount = 0;
            _successCount++;

            if (_state == CircuitBreakerState.HalfOpen && _successCount >= SuccessThreshold)
            {
                _state = CircuitBreakerState.Closed;
                _logger.LogInformation("CircuitBreaker transitioned to Closed state");
            }
        }
    }

    private void OnFailure()
    {
        lock (_lock)
        {
            _failureCount++;
            _successCount = 0;
            _lastFailureTime = DateTime.UtcNow;

            if (_failureCount >= FailureThreshold)
            {
                _state = CircuitBreakerState.Open;
                _logger.LogWarning("CircuitBreaker transitioned to Open state after {Count} failures", FailureThreshold);
            }
        }
    }

    public CircuitBreakerState GetState() => _state;

    public void Reset()
    {
        lock (_lock)
        {
            _state = CircuitBreakerState.Closed;
            _failureCount = 0;
            _successCount = 0;
            _lastFailureTime = DateTime.MinValue;
        }
    }
}

public class CircuitBreakerOpenException : Exception
{
    public CircuitBreakerOpenException(string message) : base(message) { }
}

public interface IRetryPolicyProvider
{
    IRetryPolicy GetRetryPolicy();
}

public interface IRetryPolicy
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
}

public class DefaultRetryPolicyProvider : IRetryPolicyProvider
{
    private readonly RetrySettings _settings;
    private readonly ILogger<DefaultRetryPolicyProvider> _logger;

    public DefaultRetryPolicyProvider(
        IOptions<RetrySettings> settings,
        ILogger<DefaultRetryPolicyProvider> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public IRetryPolicy GetRetryPolicy()
    {
        return new ExponentialBackoffRetryPolicy(_settings, _logger);
    }
}

public class ExponentialBackoffRetryPolicy : IRetryPolicy
{
    private readonly RetrySettings _settings;
    private readonly ILogger _logger;

    public ExponentialBackoffRetryPolicy(RetrySettings settings, ILogger logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        var attempt = 0;
        Exception? lastException = null;

        while (attempt < _settings.MaxRetries)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                attempt++;
                lastException = ex;
                
                if (attempt >= _settings.MaxRetries)
                {
                    _logger.LogError(ex, "All {MaxRetries} retry attempts failed", _settings.MaxRetries);
                    break;
                }

                var delay = _settings.ExponentialBackoff
                    ? Math.Min(
                        _settings.DelayMilliseconds * (int)Math.Pow(2, attempt - 1),
                        _settings.MaxDelayMilliseconds)
                    : _settings.DelayMilliseconds;

                _logger.LogWarning("Retry attempt {Attempt} failed. Waiting {Delay}ms before next attempt",
                    attempt, delay);

                await Task.Delay(delay, cancellationToken);
            }
        }

        throw new RetryExhaustedException(
            $"Retry policy exhausted after {_settings.MaxRetries} attempts",
            lastException);
    }
}

public class RetryExhaustedException : Exception
{
    public RetryExhaustedException(string message, Exception? innerException)
        : base(message, innerException) { }
}

// === ARCHIVO: src/WebApi/WebApi.csproj ===
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <FrameworkReference Include="Microsoft.AspNetCore.App" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="MediatR" Version="12.5.0" />
    <PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="12.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Application\Application.csproj" />
    <ProjectReference Include="..\Domain\Domain.csproj" />
    <ProjectReference Include="..\Infrastructure\Infrastructure.csproj" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/WebApi/Controllers/CodeSuggestionController.cs ===
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

// === ARCHIVO: src/WebApi/Controllers/CodeCorrectionController.cs ===
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


// === ARCHIVO: src/WebApi/Controllers/DocumentationController.cs ===
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

// === ARCHIVO: src/WebApi/Middleware/ErrorHandlingMiddleware.cs ===
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        PropertyNameCaseInsensitive = true
    };

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var correlationId = GetOrCreateCorrelationId(context);
        var originalResponseBody = context.Response.Body;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        try
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Correlation-Id"] = correlationId;
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                return Task.CompletedTask;
            });

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            if (context.Response.StatusCode >= 400)
            {
                await LogErrorResponse(context, responseBody, correlationId);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalResponseBody);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}",
                correlationId, 
                context.Request.Path);

            await HandleExceptionAsync(context, ex, correlationId);
        }
        finally
        {
            context.Response.Body = originalResponseBody;
        }
    }

    private static string GetOrCreateCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationIdHeader) &&
            !string.IsNullOrWhiteSpace(correlationIdHeader))
        {
            return correlationIdHeader.ToString();
        }

        return Guid.NewGuid().ToString("N");
    }

    private async Task LogErrorResponse(HttpContext context, string responseBody, string correlationId)
    {
        var logLevel = context.Response.StatusCode switch
        {
            >= 500 => LogLevel.Error,
            >= 400 => LogLevel.Warning,
            _ => LogLevel.Information
        };

        _logger.Log(
            logLevel,
            "HTTP {Method} {Path} responded with {StatusCode}. CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            correlationId);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new ErrorResponse
        {
            CorrelationId = correlationId,
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "An unexpected error occurred. Please try again later.",
            Timestamp = DateTime.UtcNow
        };

        switch (exception)
        {
            case ArgumentNullException argNullEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid request parameters";
                errorResponse.Details = argNullEx.Message;
                break;

            case ArgumentException argEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid argument";
                errorResponse.Details = argEx.Message;
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Unauthorized access";
                break;

            case TimeoutException:
                context.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                errorResponse.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                errorResponse.Message = "The request timed out. Please try again.";
                break;

            default:
                if (_environment.IsDevelopment())
                {
                    errorResponse.Details = exception.ToString();
                    errorResponse.StackTrace = exception.StackTrace;
                }
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(errorResponse, JsonOptions);
        await context.Response.WriteAsync(jsonResponse);
    }

    private class ErrorResponse
    {
        public string CorrelationId { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public string? StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

// === ARCHIVO: src/WebApi/appsettings.Development.json ===
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information",
      "System": "Warning",
      "System.Net.Http": "Warning"
    },
    "Console": {
      "FormatterName": "console",
      "FormatterOptions": {
        "TimestampFormat": "yyyy-MM-dd HH:mm:ss.fff zzz",
        "UseUtcTimestamp": true
      }
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CodeOptimizationDev;User Id=dev_user;Password=dev_password;TrustServerCertificate=True;MultipleActiveResultSets=true",
    "RedisConnection": "localhost:6379,abortConnect=false"
  },
  "ApplicationSettings": {
    "Environment": "Development",
    "EnableDetailedErrors": true,
    "MaxRequestSizeBytes": 52428800,
    "RequestTimeoutSeconds": 60,
    "EnableSwagger": true,
    "EnableHealthChecks": true
  },
  "CodeWhispererSettings": {
    "ServiceUrl": "http://localhost:5001",
    "TimeoutSeconds": 30,
    "MaxRetries": 3,
    "EnableFallback": true
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:3000",
      "http://localhost:4200",
      "http://localhost:8080"
    ],
    "AllowedMethods": ["GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS"],
    "AllowedHeaders": ["Origin", "Content-Type", "Accept", "Authorization", "X-Correlation-Id"],
    "ExposedHeaders": ["X-Correlation-Id", "X-Total-Count"],
    "AllowCredentials": true,
    "PreflightMaxAgeSeconds": 3600
  },
  "Authentication": {
    "JwtKey": "DevelopmentSecretKeyThatIsLongEnoughForHS256Algorithm",
    "JwtIssuer": "CodeOptimizationAPI-Dev",
    "JwtAudience": "CodeOptimizationClient-Dev",
    "TokenExpirationMinutes": 480,
    "RequireHttpsMetadata": false
  },
  "RateLimiting": {
    "EnableRateLimiting": true,
    "PermitLimit": 100,
    "WindowSeconds": 60,
    "QueueLimit": 10
  },
  "Caching": {
    "EnableCaching": true,
    "DefaultExpirationMinutes": 15,
    "SlidingExpiration": true
  },
  "Monitoring": {
    "EnableMetrics": true,
    "EnableTracing": true,
    "EnableHealthCheckEndpoint": true,
    "HealthCheckIntervalSeconds": 30
  }
}

// === ARCHIVO: tests/Application.Tests/Application.Tests.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.0" />
    <PackageReference Include="Moq" Version="4.20.70" />
    <PackageReference Include="xunit" Version="2.8.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.1" />
    <PackageReference Include="FluentAssertions" Version="6.12.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Application\Application.csproj" />
    <ProjectReference Include="..\..\src\Domain\Domain.csproj" />
  </ItemGroup>

</Project>

// === ARCHIVO: tests/Application.Tests/Services/CodeSuggestionServiceTests.cs ===
using Domain.Interfaces;
using Domain.Models;
using Application.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Services;

public class CodeSuggestionServiceTests
{
    private readonly Mock<ICodeSuggestionService> _mockSuggestionService;
    private readonly CodeSuggestionService _sut;

    public CodeSuggestionServiceTests()
    {
        _mockSuggestionService = new Mock<ICodeSuggestionService>();
        _sut = new CodeSuggestionService(_mockSuggestionService.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GenerateSuggestionsAsync_WithValidRequest_ReturnsSuggestions()
    {
        // Arrange
        var request = new CodeSuggestionRequest(
            currentCode: "public class Test { }",
            contextDescription: "Unit test class",
            suggestionsCount: 3);

        var expectedResponse = new CodeSuggestionResponse(
            suggestions: new List<CodeSuggestion>
            {
                new("public class Test { public void Method() { } }", "Add method", 0.9)
            },
            originalContext: "Unit test class");

        _mockSuggestionService
            .Setup(s => s.GenerateSuggestionsAsync(It.IsAny<CodeSuggestionRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _sut.GenerateSuggestionsAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasValidSuggestions().Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GenerateSuggestionsAsync_WithEmptyCode_ThrowsException()
    {
        // Arrange
        var request = new CodeSuggestionRequest(
            currentCode: "",
            contextDescription: "Empty code",
            suggestionsCount: 3);

        _mockSuggestionService
            .Setup(s => s.GenerateSuggestionsAsync(It.IsAny<CodeSuggestionRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CodeSuggestionException("Code cannot be empty"));

        // Act & Assert
        await Assert.ThrowsAsync<CodeSuggestionException>(
            async () => await _sut.GenerateSuggestionsAsync(request, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ValidateSuggestions_WithValidSuggestions_ReturnsValidResults()
    {
        // Arrange
        var suggestions = new List<CodeSuggestion>
        {
            new("code1", "desc1", 0.9),
            new("code2", "desc2", 0.7)
        };

        var validationResults = new Dictionary<CodeSuggestion, bool>
        {
            { suggestions[0], true },
            { suggestions[1], false }
        };

        _mockSuggestionService
            .Setup(s => s.ValidateSuggestions(It.IsAny<IEnumerable<CodeSuggestion>>()))
            .Returns(validationResults);

        // Act
        var result = _sut.ValidateSuggestions(suggestions);

        // Assert
        result.Should().HaveCount(2);
        result[suggestions[0]].Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetPerformanceMetricsAsync_ReturnsMetrics()
    {
        // Arrange
        var metrics = new List<SuggestionMetrics>
        {
            new(10, 8, 0.8),
            new(5, 4, 0.8)
        };

        _mockSuggestionService
            .Setup(s => s.GetPerformanceMetricsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(metrics);

        // Act
        var result = await _sut.GetPerformanceMetricsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [Trait("Category", "Unit")]
    public async Task GenerateSuggestionsAsync_WithDifferentSuggestionCounts_RequestsCorrectCount(int suggestionCount)
    {
        // Arrange
        var request = new CodeSuggestionRequest(
            currentCode: "public void Test() { }",
            contextDescription: "Test method",
            suggestionsCount: suggestionCount);

        _mockSuggestionService
            .Setup(s => s.GenerateSuggestionsAsync(
                It.Is<CodeSuggestionRequest>(r => r.SuggestionsCount == suggestionCount),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CodeSuggestionResponse(
                Enumerable.Empty<CodeSuggestion>(),
                "Test"));

        // Act
        await _sut.GenerateSuggestionsAsync(request, CancellationToken.None);

        // Assert
        _mockSuggestionService.Verify(
            s => s.GenerateSuggestionsAsync(
                It.Is<CodeSuggestionRequest>(r => r.SuggestionsCount == suggestionCount),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

// === ARCHIVO: tests/Infrastructure.Tests/Infrastructure.Tests.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.0" />
    <PackageReference Include="xunit" Version="2.8.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.1">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Moq" Version="4.20.70" />
    <PackageReference Include="FluentAssertions" Version="6.12.0" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Infrastructure\Infrastructure.csproj" />
    <ProjectReference Include="..\..\src\Domain\Domain.csproj" />
    <ProjectReference Include="..\..\src\Application\Application.csproj" />
  </ItemGroup>

</Project>

// === ARCHIVO: tests/Infrastructure.Tests/Services/AwsCodeWhispererServiceTests.cs ===
using Xunit;
using Moq;
using FluentAssertions;
using Domain.Models;
using Infrastructure.Services;

namespace Infrastructure.Tests.Services;

[Trait("Category", "Integration")]
public class AwsCodeWhispererServiceTests
{
    private readonly Mock<AwsCodeWhispererService> _serviceMock;
    private readonly string _testApiKey;

    public AwsCodeWhispererServiceTests()
    {
        _testApiKey = "test-api-key-for-integration-testing";
        _serviceMock = new Mock<AwsCodeWhispererService>(_testApiKey);
    }

    [Fact]
    public async Task GenerateSuggestionsAsync_WithValidRequest_ReturnsSuggestions()
    {
        var request = new CodeSuggestionRequest(
            currentCode: "public class Test { }",
            contextDescription: "Unit test class",
            suggestionsCount: 3);

        var result = await _serviceMock.Object.GenerateSuggestionsAsync(request, CancellationToken.None);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CorrectCodeAsync_WithValidRequest_ReturnsCorrection()
    {
        var request = new CodeCorrectionRequest(
            problematicCode: "var x =",
            errorDescription: "Syntax error",
            severityLevel: 1);

        var result = await _serviceMock.Object.CorrectCodeAsync(request, CancellationToken.None);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GenerateDocumentationAsync_WithValidRequest_ReturnsDocumentation()
    {
        var request = new DocumentationRequest(
            codeSnippet: "public void Process() { // Processes data }",
            comment: "Processes the incoming data stream",
            scopeLines: 10);

        var result = await _serviceMock.Object.GenerateDocumentationAsync(request, CancellationToken.None);

        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GenerateSuggestionsAsync_WithInvalidCode_ThrowsException(string invalidCode)
    {
        var request = new CodeSuggestionRequest(
            currentCode: invalidCode,
            contextDescription: "Test",
            suggestionsCount: 3);

        await Assert.ThrowsAsync<CodeSuggestionException>(
            async () => await _serviceMock.Object.GenerateSuggestionsAsync(request, CancellationToken.None));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public async Task GenerateSuggestionsAsync_WithInvalidSuggestionCount_ThrowsException(int invalidCount)
    {
        var request = new CodeSuggestionRequest(
            currentCode: "public class Test { }",
            contextDescription: "Test",
            suggestionsCount: invalidCount);

        await Assert.ThrowsAsync<CodeSuggestionException>(
            async () => await _serviceMock.Object.GenerateSuggestionsAsync(request, CancellationToken.None));
    }
}

// === ARCHIVO: src/Domain/Interfaces/IDocumentationGeneratorService.cs ===
using Domain.Models;

namespace Domain.Interfaces;

/// <summary>
/// Define el contrato para la generación automática de documentación
/// a partir de comentarios y código fuente.
/// </summary>
public interface IDocumentationGeneratorService
{
    /// <summary>
    /// Genera documentación automáticamente a partir de un fragmento de código
    /// y sus comentarios asociados.
    /// </summary>
    /// <param name="request">Solicitud con el código y comentarios a documentar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con la documentación generada.</returns>
    /// <exception cref="DocumentationGenerationException">Cuando la generación de documentación falla.</exception>
    Task<DocumentationResponse> GenerateDocumentationAsync(
        DocumentationRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Analiza el código fuente y extrae los elementos que requieren documentación.
    /// </summary>
    /// <param name="codeSnippet">Código fuente a analizar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Colección de elementos que necesitan documentación.</returns>
    Task<IEnumerable<DocumentationElement>> AnalyzeCodeForDocumentationAsync(
        string codeSnippet,
        CancellationToken cancellationToken);

    /// <summary>
    /// Valida que la documentación generada cumpla con los estándares de calidad.
    /// </summary>
    /// <param name="documentation">Documentación a validar.</param>
    /// <returns>Resultado de la validación con problemas encontrados.</returns>
    DocumentationValidationResult ValidateDocumentation(string documentation);

    /// <summary>
    /// Estima el nivel de detalle de documentación que se puede generar
    /// basándose en la complejidad del código.
    /// </summary>
    /// <param name="codeSnippet">Código a analizar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Nivel de detalle estimado (1-5).</returns>
    Task<int> EstimateDocumentationLevelAsync(
        string codeSnippet,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representa un elemento de código que requiere documentación.
/// </summary>
public class DocumentationElement
{
    public string ElementName { get; set; } = string.Empty;
    public DocumentationElementType ElementType { get; set; }
    public int StartLine { get; set; }
    public int EndLine { get; set; }
    public bool HasExistingDocumentation { get; set; }
    public string ExistingDocumentation { get; set; } = string.Empty;
    public List<string> Parameters { get; set; } = new();
    public string ReturnType { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
}

/// <summary>
/// Tipos de elementos de código que pueden requerir documentación.
/// </summary>
public enum DocumentationElementType
{
    Class,
    Interface,
    Method,
    Property,
    Field,
    Constructor,
    Enum,
    Struct,
    Delegate,
    Event
}

/// <summary>
/// Resultado de la validación de documentación.
/// </summary>
public class DocumentationValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public List<string> ValidationWarnings { get; set; } = new();
    public double QualityScore { get; set; }

    public bool HasErrors() => ValidationErrors.Count > 0;
    public bool HasWarnings() => ValidationWarnings.Count > 0;

    public string GetSummary()
    {
        var summary = $"Documentation validation: {(IsValid ? "PASSED" : "FAILED")}";
        if (HasErrors())
            summary += $" | Errors: {string.Join(", ", ValidationErrors)}";
        if (HasWarnings())
            summary += $" | Warnings: {string.Join(", ", ValidationWarnings)}";
        summary += $" | Quality Score: {QualityScore:P0}";
        return summary;
    }
}

// === ARCHIVO: src/CodeOptimizationAI.sln ===
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17.0
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Domain", "Domain\Domain.csproj", "{8A3B7C4D-9E2F-1A5B-6C8D-7E0F1A2B3C4D}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Application", "Application\Application.csproj", "{9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Infrastructure", "Infrastructure\Infrastructure.csproj", "{0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "WebApi", "WebApi\WebApi.csproj", "{1D6E0F7A-2B5C-4D8E-9F0A-1B2C3D4E5F7A}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Application.Tests", "..\tests\Application.Tests\Application.Tests.csproj", "{2E7F1A8B-3C6D-5E9F-0A1B-2C3D4E5F6A8B}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Infrastructure.Tests", "..\tests\Infrastructure.Tests\Infrastructure.Tests.csproj", "{3F8A2B9C-4D7E-6F0A-1B2C-3D4E5F6A7B9C}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{8A3B7C4D-9E2F-1A5B-6C8D-7E0F1A2B3C4D}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{8A3B7C4D-9E2F-1A5B-6C8D-7E0F1A2B3C4D}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{8A3B7C4D-9E2F-1A5B-6C8D-7E0F1A2B3C4D}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{8A3B7C4D-9E2F-1A5B-6C8D-7E0F1A2B3C4D}.Release|Any CPU.Build.0 = Release|Any CPU
		{9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}.Release|Any CPU.Build.0 = Release|Any CPU
		{0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}.Release|Any CPU.Build.0 = Release|Any CPU
		{1D6E0F7A-2B5C-4D8E-9F0A-1B2C3D4E5F7A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{1D6E0F7A-2B5C-4D8E-9F0A-1B2C3D4E5F7A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{1D6E0F7A-2B5C-4D8E-9F0A-1B2C3D4E5F7A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{1D6E0F7A-2B5C-4D8E-9F0A-1B2C3D4E5F7A}.Release|Any CPU.Build.0 = Release|Any CPU
		{2E7F1A8B-3C6D-5E9F-0A1B-2C3D4E5F6A8B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{2E7F1A8B-3C6D-5E9F-0A1B-2C3D4E5F6A8B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{2E7F1A8B-3C6D-5E9F-0A1B-2C3D4E5F6A8B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{2E7F1A8B-3C6D-5E9F-0A1B-2C3D4E5F6A8B}.Release|Any CPU.Build.0 = Release|Any CPU
		{3F8A2B9C-4D7E-6F0A-1B2C-3D4E5F6A7B9C}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{3F8A2B9C-4D7E-6F0A-1B2C-3D4E5F6A7B9C}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{3F8A2B9C-4D7E-6F0A-1B2C-3D4E5F6A7B9C}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{3F8A2B9C-4D7E-6F0A-1B2C-3D4E5F6A7B9C}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E} = {8A3B7C4D-9E2F-1A5B-6C8D-7E0F1A2B3C4D}
		{0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F} = {9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}
		{1D6E0F7A-2B5C-4D8E-9F0A-1B2C3D4E5F7A} = {0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}
		{2E7F1A8B-3C6D-5E9F-0A1B-2C3D4E5F6A8B} = {9B4C8D5E-0F3A-2B6C-7D9E-8F1A0B2C3D5E}
		{3F8A2B9C-4D7E-6F0A-1B2C-3D4E5F6A7B9C} = {0C5D9E6F-1A4B-3C7D-8E0F-9A1B2C3D4E6F}
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = preSolution
		SolutionGuid = {ABCD1234-EF56-7890-ABCD-EF1234567890}
	EndGlobalSection
EndGlobal
```
