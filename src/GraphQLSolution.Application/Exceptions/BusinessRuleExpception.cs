namespace GraphQLSolution.Application.Exceptions;

public class BusinessRuleException : BaseException
{
    public BusinessRuleException(string message) 
        : base(message, "BUSINESS_RULE_VIOLATION", 400)
    {
    }

    public BusinessRuleException(string message, Exception innerException) 
        : base(message, innerException, "BUSINESS_RULE_VIOLATION", 400)
    {
    }
}