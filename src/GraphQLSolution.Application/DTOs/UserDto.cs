namespace GraphQLSolution.Application.DTOs;

public record UserDto(
    string Id,
    string Email,
    string FullName,
    DateTime CreatedAt,
    bool IsActive
);

public record RegisterUserDto(
    string Email,
    string Password,
    string FullName
);

public record LoginUserDto(
    string Email,
    string Password
);

