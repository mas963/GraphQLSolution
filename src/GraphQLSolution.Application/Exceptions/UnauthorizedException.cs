namespace GraphQLSolution.Application.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message, "UNAUTHORIZED", 401)
    {
    }
    
    public UnauthorizedException(string message, Exception innerException) : base(message, innerException, "UNAUTHORIZED", 401)
    {
    }
}