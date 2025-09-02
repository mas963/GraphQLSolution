using GraphQLSolution.Application.DTOs;
using GraphQLSolution.Application.Services;

namespace GraphQLSolution.Api.GraphQL.Mutations;

public class UserMutations
{
    public async Task<string> Login(LoginUserDto loginUserDto,
        [Service] IUserService userService)
    {
        return await userService.LoginUserAsync(loginUserDto);
    }

    public async Task<string> Register(RegisterUserDto registerUserDto,
        [Service] IUserService userService)
    {
        return await userService.RegisterUserAsync(registerUserDto);
    }
}
