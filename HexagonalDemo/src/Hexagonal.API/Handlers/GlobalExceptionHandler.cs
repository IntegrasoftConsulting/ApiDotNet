namespace Hexagonal.API.Handlers;

using Microsoft.AspNetCore.Diagnostics;
using Hexagonal.API.Models;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Ocurrió un error no controlado: {Message}", exception.Message);

        // Personalizamos la respuesta según el tipo de excepción
        var (statusCode, message) = exception switch
        {
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "No tienes permiso para esto."),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "El recurso no existe."),
            _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor. Por favor, intenta más tarde.")
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            new ErrorResponse(statusCode, message, exception.Message), 
            cancellationToken);

        return true; // Indica que la excepción ha sido manejada
    }
}