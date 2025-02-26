namespace Backend.src.Middleware.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }

    public class BadRequestException(string message) : Exception(message)
    {
    }

    public class UnauthorizedException(string message) : Exception(message)
    {
    }

    public class KeyNotFoundException(string message) : Exception(message)
    {
    }

    public class InvalidPasswordException(string message) : Exception(message)
    {
    }

    public class InvalidOperationException(string message) : Exception(message)
    {
    }
}
