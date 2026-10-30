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