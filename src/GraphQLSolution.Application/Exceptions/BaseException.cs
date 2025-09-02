namespace GraphQLSolution.Application.Exceptions;

public class BaseException : Exception
{
    public string ErrorCode { get; set; }
    public int StatusCode { get; set; }
    
    protected BaseException(string message, string errorCode, int statusCode) 
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    protected BaseException(string message, Exception innerException, string errorCode, int statusCode) 
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}