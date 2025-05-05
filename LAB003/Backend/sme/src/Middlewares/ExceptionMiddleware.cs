using System.Net;
using System.Text.Json;
using sme.src.Middlewares.Exceptions;
namespace sme.src.Middlewares
{

    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

        /// <summary>
        /// Processa a requisição HTTP passando o controle para o próximo middleware na cadeia e captura quaisquer exceções que possam ocorrer,
        /// garantindo que erros inesperados sejam devidamente tratados e registrados.
        /// </summary>
        /// <param name="context">O contexto da requisição HTTP atual.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona do processamento da requisição.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Trata as exceções que ocorrem durante o processamento de uma requisição HTTP,
        /// definindo o código de status apropriado e a mensagem de erro na resposta.
        /// O método mapeia tipos de exceção específicos, como NotFoundException, BadRequestException,
        /// UnauthorizedException, Exceptions.KeyNotFoundException, InvalidPasswordException e Exceptions.InvalidOperationException,
        /// para os respectivos códigos de status HTTP. Para quaisquer outras exceções, é utilizado o Internal Server Error.
        /// </summary>
        /// <param name="context">O contexto HTTP associado à requisição atual.</param>
        /// <param name="exception">A exceção que foi lançada durante o processamento da requisição.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona de escrita da resposta de erro.</returns>
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
                case ValidationException validationResult:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = validationResult.Message;
                    break;
                case MailerException mailerException:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = mailerException.Message;
                    break;
                case ConflictException conflictException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = conflictException.Message;
                    break;
                case CustomArgumentNullException argumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = argumentNullException.Message;
                    break;
                case Exceptions.NotImplementedException notImplementedException:
                    statusCode = (int)HttpStatusCode.NotImplemented;
                    message = notImplementedException.Message;
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
}