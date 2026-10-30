# Optimización de Flujo de Trabajo con IA Generativa en Desarrollo Backend .Net

El candidato, un desarrollador backend avanzado con experiencia en.Net, busca integrar herramientas de IA generativa en su flujo de trabajo para mejorar la calidad del código y aumentar la productividad. El sistema debe incorporar sugerencias de código, correcciones automáticas y generación de documentación a partir de comentarios. Los actores involucrados son el Desarrollador (Juan Pérez), la Herramienta de IA (CodeWhisperer) y el Repositorio de Código (GitHub). El sistema debe manejar volúmenes de código típicos en proyectos de tamaño mediano, con una latencia máxima de 2 segundos en la generación de sugerencias.

## Informacion General

| Campo | Valor |
|-------|-------|
| **Tema** | Desarrollo |
| **Nivel** | advanced-l2 |
| **Tipo** | practical |
| **Tiempo estimado** | 10 horas |

## Fases del Reto

### Fase 0: Configuración del Proyecto

**Objetivo:** Obtener el proyecto base funcional enviando el Código Base a un asistente de IA, que lo analizará, corregirá errores y generará un ZIP listo para usar.

**Tiempo estimado:** 15-30 minutos

**Instrucciones:**

- Asegúrate de tener instalado para ejecutar el proyecto: .NET SDK 8+, VS Code o Visual Studio.
- Copia todo el contenido del campo **Código Base** de este reto — incluyendo el texto de instrucciones que aparece al inicio.
- Abre un asistente de IA (Claude en claude.ai, ChatGPT o Gemini — se recomienda Claude), pega el contenido copiado en el chat y envíalo.
- El asistente analizará los archivos, corregirá errores y generará un archivo ZIP descargable. Descárgalo y extráelo en la carpeta donde quieras trabajar.
- Ejecuta `dotnet build`. Si no hay errores, estás listo.

**Entregable:** El proyecto compila/arranca sin errores.

<details>
<summary>Pistas de conocimiento</summary>

- Copia el Código Base completo incluyendo el texto de instrucciones al inicio — esas instrucciones le indican al asistente exactamente qué hacer con los archivos.
- Si el asistente no genera el ZIP automáticamente al terminar el análisis, escríbele: "genera el ZIP ahora".
- Si el proyecto tiene errores al arrancar, comparte el mensaje de error con el mismo asistente para que lo corrija.

</details>

### Fase 1: Integración Inicial de la Herramienta de IA

**Objetivo:** Incorporar la herramienta de IA en el entorno de desarrollo para proporcionar sugerencias de código.

**Tiempo estimado:** 3 horas

**Instrucciones:**

- Identificar los puntos de integración entre la herramienta de IA y el IDE utilizado por el desarrollador.
- Configurar la herramienta para que ofrezca sugerencias en tiempo real mientras el desarrollador escribe código.

**Entregable:** Configuración de la herramienta de IA en el IDE con sugerencias de código en tiempo real.

<details>
<summary>Pistas de conocimiento</summary>

- Considera las APIs disponibles para la integración de la herramienta de IA.
- Evalúa la latencia en la entrega de sugerencias para asegurar una experiencia fluida.

</details>

### Fase 2: Automatización de Correcciones de Código

**Objetivo:** Implementar un sistema que automatice la corrección de errores comunes en el código.

**Tiempo estimado:** 4 horas

**Instrucciones:**

- Identificar los errores comunes en el código que pueden ser corregidos automáticamente.
- Configurar la herramienta de IA para que aplique correcciones automáticamente al detectar estos errores.

**Entregable:** Sistema automatizado que corrige errores comunes en el código utilizando la herramienta de IA.

<details>
<summary>Pistas de conocimiento</summary>

- Evalúa los tipos de errores que pueden ser corregidos de forma segura por la herramienta de IA.
- Considera cómo manejar los casos en los que la corrección automática podría ser inapropiada.

</details>

### Fase 3: Generación de Documentación a partir de Comentarios

**Objetivo:** Configurar la herramienta de IA para generar documentación a partir de comentarios en el código.

**Tiempo estimado:** 3 horas

**Instrucciones:**

- Identificar los comentarios en el código que pueden ser utilizados para generar documentación.
- Configurar la herramienta de IA para que extraiga información de estos comentarios y genere documentación automáticamente.

**Entregable:** Sistema que genera documentación automáticamente a partir de comentarios en el código utilizando la herramienta de IA.

<details>
<summary>Pistas de conocimiento</summary>

- Considera el formato y la estructura de los comentarios que serán utilizados para la generación de documentación.
- Evalúa la calidad y precisión de la documentación generada por la herramienta de IA.

</details>

## Dimensiones Evaluadas

- **queEs**: ¿Qué es una herramienta de IA generativa y cómo se integra en un flujo de trabajo de desarrollo?
- **paraQueSirve**: ¿Para qué sirve la integración de una herramienta de IA generativa en el desarrollo de backend con.Net?
- **comoSeUsa**: ¿Cómo se utiliza una herramienta de IA generativa para mejorar la calidad del código y aumentar la productividad?
- **erroresComunes**: ¿Cuáles son los errores comunes que pueden ser corregidos automáticamente por una herramienta de IA generativa?
- **queDecisionesImplica**: ¿Qué decisiones debes tomar al integrar una herramienta de IA generativa en tu flujo de trabajo de desarrollo?

## Criterios de Evaluacion

- Integración de la herramienta de IA en el IDE con sugerencias de código en tiempo real.
- Sistema automatizado que corrige errores comunes en el código utilizando la herramienta de IA.
- Sistema que genera documentación automáticamente a partir de comentarios en el código utilizando la herramienta de IA.

## Como trabajar con un asistente de IA

Hay dos caminos, elegi uno:

- **AGENTS.md** (recomendado) — instrucciones nativas del repo. Abri esta carpeta con tu agente local (Claude Code, Cursor, Codex, Copilot, Gemini) y las carga solo. Sabe que archivos faltan y con que comando se verifica, y completa el scaffold escribiendo en disco.
- **PROMPT_MEJORA.md** — para copiar y pegar en un chat (claude.ai, ChatGPT). Devuelve un ZIP con el proyecto. Sirve si no tenes un agente en el IDE.

Ninguno de los dos resuelve las fases del reto: eso es tu trabajo.

## Verificacion

El proyecto esta listo para trabajar cuando este comando corre sin errores:

```bash
dotnet build
```

---

*Reto generado automaticamente por Challenge Generator - Pragma*
