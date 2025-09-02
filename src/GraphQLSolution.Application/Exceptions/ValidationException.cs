namespace GraphQLSolution.Application.Exceptions;

public class ValidationException : BaseException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string message) 
        : base(message, "VALIDATION_ERROR", 400)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.", "VALIDATION_ERROR", 400)
    {
        Errors = errors;
    }

    public ValidationException(string field, string error)
        : base($"Validation failed for field '{field}': {error}", "VALIDATION_ERROR", 400)
    {
        Errors = new Dictionary<string, string[]>
        {
            [field] = new[] { error }
        };
    }
}