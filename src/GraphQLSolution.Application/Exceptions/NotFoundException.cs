using System;

namespace GraphQLSolution.Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) 
        : base(message, "NOT_FOUND", 404)
    {
    }

    public NotFoundException(string entityName, string id) 
        : base($"{entityName} with id '{id}' was not found.", "NOT_FOUND", 404)
    {
    }
}