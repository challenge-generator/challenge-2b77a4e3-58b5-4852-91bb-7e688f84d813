# AGENTS.md

Instrucciones para el agente de IA que abra este repositorio (Claude Code, Cursor, Codex, Copilot, Gemini). Se cargan solas: no hay que pegar nada en ningun chat.

## Que es este repositorio

Es el codigo base de un reto de aprendizaje de Pragma: **Optimización de Flujo de Trabajo con IA Generativa en Desarrollo Backend .Net**.

| | |
|---|---|
| Tema | Desarrollo |
| Nivel | advanced-l2 |
| Chapter | Backend |
| Especialidad | .NET |
| Stack | .NET 9 / ASP.NET Core 9 |
| Patron arquitectonico | capas estándar con inyección de dependencias y patrones CQRS |
| Tiempo estimado | 10 horas |

## Receta del stack

Esqueleto obligatorio:

- `*.csproj en la raiz del proyecto`
- `Program.cs como entry point`
- `appsettings.json`
- `carpeta Domain con entidades e interfaces`
- `carpeta Application con casos de uso`
- `carpeta Infrastructure con repositorios y Controllers`

Dependencias:

- Microsoft.NET.Sdk.Web 9.0.0
- Microsoft.AspNetCore.App 9.0.0
- MediatR 12.5.0
- MediatR.Extensions.Microsoft.DependencyInjection 12.0.0
- AWSSDK.CodeWhisperer n/a
- xunit 2.8.1
- Moq 4.20.70
- Microsoft.AspNetCore.Mvc.Testing 9.0.0
- FluentAssertions 6.12.0
- Microsoft.Extensions.Configuration.Json 9.0.0

## Tu tarea

Dejar este proyecto en estado **verificable**: que el comando de verificacion corra sin errores. Escribi los archivos en disco, en este repositorio. No generes ZIPs ni archivos adjuntos.

En orden:

1. Corre `dotnet build` y mira que falla.
2. Completa lo que falte de la lista de abajo: manifiesto de dependencias, punto de entrada, capa de interfaz y las capas del patron declarado.
3. Arregla SOLO los errores que impiden compilar o arrancar.
4. Volve a correr `dotnet build` hasta que pase.
5. Pará ahí.

## Regla dura: las fases son trabajo del humano

**PROHIBIDO implementar los entregables de las fases.** El valor del reto esta en que la persona los resuelva. Tu trabajo es que tenga un proyecto que arranca; el hueco pedagogico se queda como esta.

No resuelvas nada de esto:

- **Fase 1 — Integración Inicial de la Herramienta de IA**: Configuración de la herramienta de IA en el IDE con sugerencias de código en tiempo real.
- **Fase 2 — Automatización de Correcciones de Código**: Sistema automatizado que corrige errores comunes en el código utilizando la herramienta de IA.
- **Fase 3 — Generación de Documentación a partir de Comentarios**: Sistema que genera documentación automáticamente a partir de comentarios en el código utilizando la herramienta de IA.

Distincion operativa:

- **Arreglar** (si): import faltante, tipo que no existe, dependencia sin declarar, error de sintaxis, archivo referenciado que no existe.
- **No tocar** (no): logica de negocio incompleta, validaciones ausentes, secretos hardcodeados, APIs deprecadas que funcionan, concurrencia insegura, patrones mejorables. Eso es lo que la persona tiene que encontrar.

## Superficie de practica (NO completes)

Estos archivos SON el ejercicio de la persona. No los implementes; deja stubs. No toques la logica que el reto pide completar.

- [ ] `tests/Application.Tests/Services/CodeSuggestionServiceTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- [ ] `tests/Infrastructure.Tests/Services/AwsCodeWhispererServiceTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- [ ] `tests/Application.Tests/Application.Tests.csproj` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- [ ] `tests/Infrastructure.Tests/Infrastructure.Tests.csproj` — El topic pide TDD/pruebas: este archivo es el ejercicio.

## Lo que falta y tenes que completar

No se detectaron huecos: estan los archivos declarados, el boilerplate del stack y ninguna referencia quedo colgando. Igual corre el comando de verificacion — que los archivos existan no garantiza que compilen.

### Presentes (36)

- `src/WebApi/Program.cs`
- `src/WebApi/appsettings.json`
- `src/Domain/Domain.csproj`
- `src/Domain/Models/DocumentationRequest.cs`
- `src/Domain/Models/CodeSuggestionRequest.cs`
- `src/Domain/Models/CodeSuggestionResponse.cs`
- `src/Domain/Models/CodeCorrectionRequest.cs`
- `src/Domain/Models/CodeCorrectionResponse.cs`
- `src/Domain/Entities/CodeSuggestion.cs`
- `src/Domain/Interfaces/ICodeSuggestionService.cs`
- `src/Domain/Interfaces/ICodeCorrectionService.cs`
- `src/Application/Application.csproj`
- `src/Application/DTOs/CodeSuggestionRequest.cs`
- `src/Application/DTOs/CodeSuggestionResponse.cs`
- `src/Application/DTOs/CodeCorrectionRequest.cs`
- `src/Application/DTOs/CodeCorrectionResponse.cs`
- `src/Application/DTOs/DocumentationRequest.cs`
- `src/Application/DTOs/DocumentationResponse.cs`
- `src/Application/Services/CodeSuggestionService.cs`
- `src/Application/Services/CodeCorrectionService.cs`
- `src/Application/Services/DocumentationGeneratorService.cs`
- `src/Infrastructure/Infrastructure.csproj`
- `src/Infrastructure/Services/AwsCodeWhispererService.cs`
- `src/Infrastructure/Extensions/ServiceCollectionExtensions.cs`
- `src/WebApi/WebApi.csproj`
- `src/WebApi/Controllers/CodeSuggestionController.cs`
- `src/WebApi/Controllers/CodeCorrectionController.cs`
- `src/WebApi/Controllers/DocumentationController.cs`
- `src/WebApi/Middleware/ErrorHandlingMiddleware.cs`
- `src/WebApi/appsettings.Development.json`
- `tests/Application.Tests/Application.Tests.csproj`
- `tests/Application.Tests/Services/CodeSuggestionServiceTests.cs`
- `tests/Infrastructure.Tests/Infrastructure.Tests.csproj`
- `tests/Infrastructure.Tests/Services/AwsCodeWhispererServiceTests.cs`
- `src/Domain/Interfaces/IDocumentationGeneratorService.cs`
- `src/CodeOptimizationAI.sln`

### Capas del patron declarado

Cada una tiene que existir como directorio real con al menos un archivo. Codigo plano en la raiz no satisface el patron.

- `src`
- `src/Domain`
- `src/Domain/Entities`
- `src/Domain/Interfaces`
- `src/Application`
- `src/Application/DTOs`
- `src/Application/Services`
- `src/Infrastructure`
- `src/Infrastructure/Services`
- `src/Infrastructure/Repositories`
- `src/Infrastructure/Extensions`
- `src/WebApi`
- `src/WebApi/Controllers`
- `src/WebApi/Middleware`
- `tests`
- `tests/Application.Tests`
- `tests/Infrastructure.Tests`

## Verificacion

```bash
dotnet build
```

El comando tiene que pasar SIN implementar los archivos de la superficie de practica: solo andamiaje.

Ese comando pasando es la definicion de "terminado" para vos.

## Convenciones que tenes que respetar

- Un solo ecosistema: no declares librerias de otro lenguaje ni mezcles gestores de paquetes.
- Toda libreria que uses tiene que estar declarada en el manifiesto de dependencias.
- Todo import declarado tiene que usarse; todo tipo usado tiene que existir o venir de una dependencia declarada.
- El patron es **capas estándar con inyección de dependencias y patrones CQRS**: los contratos (interfaces, puertos) los define la capa interna y los implementa la externa, nunca al revés.
- Los archivos que crees llevan implementacion real, no stubs: sin `TODO`, sin cuerpos vacios, sin `// getters y setters`.

## Contexto del candidato

Sirve para calibrar el nivel del codigo, no para resolver las fases.

- Perfil: Chapter Backend, Especialidad Desarrollador, Tecnología .Net, Advanced
- Brecha que el reto ataca: Ha trabajado con algún asistente AI en desarrollo (Amazon Q Dev, Kiro, Github Copilot) y lo utiliza en su día a día como desarrollador. Cierre de brechas de conocimiento en el uso efectivo de herramientas de IA para acelerar el desarrollo, mejorar la calidad del código y aumentar la productividad
- Mision: Candidato con experiencia avanzada en Backend .Net, buscando optimizar su flujo de trabajo con IA generativa

---

*Generado por Challenge Generator — Pragma. `README.md` tiene el enunciado completo del reto para la persona. `PROMPT_MEJORA.md` es la variante para pegar en un chat, si se prefiere ese flujo.*
