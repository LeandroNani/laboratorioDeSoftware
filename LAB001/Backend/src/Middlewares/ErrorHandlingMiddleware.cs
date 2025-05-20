using System.Net;
using System.Text.Json;
using Backend.src.Middlewares.Exceptions;

namespace Backend.src.Middlewares;

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
        string message = "Ocorreu um erro inesperado. Error: " + exception.Message;

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
            case InvalidPasswordException invalidPasswordExceptionEx:
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = invalidPasswordExceptionEx.Message;
                break;
            case Exceptions.InvalidOperationException invalidOperationExceptionEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = invalidOperationExceptionEx.Message;
                break;
            case SmtpException mailerException:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = mailerException.Message;
                break;
            default:
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new { error = message, statusCode });

        return context.Response.WriteAsync(result);
    }
}
