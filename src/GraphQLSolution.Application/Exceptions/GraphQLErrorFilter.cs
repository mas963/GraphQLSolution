using HotChocolate;
using Microsoft.Extensions.Logging;

namespace GraphQLSolution.Application.Exceptions;

public class GraphQLErrorFilter : IErrorFilter
{
    private readonly ILogger<GraphQLErrorFilter> _logger;

    public GraphQLErrorFilter(ILogger<GraphQLErrorFilter> logger)
    {
        _logger = logger;
    }

    public IError OnError(IError error)
    {
        var exception = error.Exception;

        _logger.LogError(exception, "GraphQL Error occurred: {Message}", error.Message);

        return exception switch
        {
            ValidationException validationEx => ErrorBuilder.New()
                .SetMessage("Validation failed")
                .SetCode("VALIDATION_ERROR")
                .SetExtension("statusCode", 400)
                .SetExtension("errors", validationEx.Errors)
                .Build(),

            BaseException baseEx => ErrorBuilder.New()
                .SetMessage(baseEx.Message)
                .SetCode(baseEx.ErrorCode)
                .SetExtension("statusCode", baseEx.StatusCode)
                .Build(),

            _ => ErrorBuilder.New()
                .SetMessage("An unexpected error occurred")
                .SetCode("INTERNAL_ERROR")
                .SetExtension("statusCode", 500)
                .Build()
        };
    }
}