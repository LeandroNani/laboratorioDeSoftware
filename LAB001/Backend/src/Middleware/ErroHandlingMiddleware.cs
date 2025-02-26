using System.Net;
using System.Text.Json;
using Backend.src.Middleware.Exceptions;

namespace Backend.src.Middleware;
public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro inesperado.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string message;

        switch (exception)
        {
            case NotFoundException notFoundEx:
                statusCode = (int)HttpStatusCode.NotFound;
                message = notFoundEx.Message;
                break;

            case BadRequestException badRequestEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = badRequestEx.Message;
                break;

            case UnauthorizedException unauthorizedEx:
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = unauthorizedEx.Message;
                break;
            case Exceptions.KeyNotFoundException keyNotFoundEx:
                statusCode = (int)HttpStatusCode.NotFound;
                message = keyNotFoundEx.Message;
                break;
            case InvalidPasswordException invalidPasswordException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = invalidPasswordException.Message;
                break;
            default:
                message = exception.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new
        {
            error = message,
            statusCode
        });

        return context.Response.WriteAsync(result);
    }
}
