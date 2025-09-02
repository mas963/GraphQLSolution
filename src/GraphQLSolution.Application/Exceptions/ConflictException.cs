using System;

namespace GraphQLSolution.Application.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(string message) 
        : base(message, "CONFLICT", 409)
    {
    }

    public ConflictException(string resource, string value) 
        : base($"{resource} '{value}' already exists.", "CONFLICT", 409)
    {
    }
}
