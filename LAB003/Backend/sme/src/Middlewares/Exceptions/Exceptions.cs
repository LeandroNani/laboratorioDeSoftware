namespace sme.src.Middlewares.Exceptions
{
    public class NotFoundException(string message)           : Exception(message);
    public class BadRequestException(string message)         : Exception(message);
    public class UnauthorizedException(string message)       : Exception(message);
    public class KeyNotFoundException(string message)        : Exception(message);
    public class InvalidPasswordException(string message)    : Exception(message);
    public class InvalidOperationException(string message)   : Exception(message);
    public class ValidationException(string message)         : Exception(message);
    public class MailerException(string message)             : Exception(message);
    public class ConflictException(string message)           : Exception(message);
    public class CustomArgumentNullException(string message) : Exception(message);
    public class NotImplementedException(string message)     : Exception(message);
}