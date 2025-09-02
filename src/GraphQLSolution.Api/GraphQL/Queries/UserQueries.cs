using GraphQLSolution.Application.DTOs;
using GraphQLSolution.Application.Services;
using HotChocolate.Authorization;
using HotChocolate.Data;

namespace GraphQLSolution.Api.GraphQL.Queries;

public class UserQueries
{
    public async Task<UserDto> GetUserAsync(
        string id,
        [Service] IUserService userService
    )
    {
        return await userService.GetUserByIdAsync(id);
    }

    [Authorize(Roles = new[] { "admin" })]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<UserDto>> GetUsersAsync(
        [Service] IUserService userService
    )
    {
        return await userService.GetAllUsersAsync();
    }
}
