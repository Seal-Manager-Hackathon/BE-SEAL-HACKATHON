namespace Hackathon.Service.Exceptions;

public class ServerException : AppException
{
    public ServerException(string message)
        : base(title: "Internal Server Error", statusCode: 500, messageCode: "INTERNAL_SERVER_ERROR", message: message) { }
}

public class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base("Not Found",404, "NOT_FOUND", message) { }
}

public class ForbiddenException : AppException
{
    public ForbiddenException(string message)
        : base("Forbidden", 403, "FORBIDDEN", message) { }
}

public class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base( "Bad Request", 400, "BAD_REQUEST", message) { }
}

public class TooManyRequestException : AppException
{
    public TooManyRequestException(string message)
        : base("Too many request", 429, "TOO_MANY_REQUEST", message) { }
}

public class ConflictException : AppException
{
    public ConflictException(string message)
        : base("Conflict", 409, "CONFLICT", message) { }
}

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message)
        : base("Unauthorized", 401, "UNAUTHORIZED", message) { }
}