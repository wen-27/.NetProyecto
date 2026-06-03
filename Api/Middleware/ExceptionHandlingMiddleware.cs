using Application.Common.Exceptions;
using FluentValidation;

namespace Api.Middleware;

// Middleware que se ejecuta dentro de la tuberia HTTP para aplicar una preocupacion transversal.
public sealed class ExceptionHandlingMiddleware
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException or KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado."),
            ForbiddenException => (StatusCodes.Status403Forbidden, "Acceso denegado."),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "No autorizado."),
            ConflictException => (StatusCodes.Status409Conflict, "Conflicto de datos."),
            ValidationException => (StatusCodes.Status400BadRequest, "Solicitud inválida."),
            InvalidOperationException => (StatusCodes.Status400BadRequest, "Operación inválida."),
            _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor.")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}", context.Request.Method, context.Request.Path);
        }

        if (context.Response.HasStarted)
        {
            throw exception;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            title,
            message = exception.Message,
            status = statusCode
        });
    }
}
