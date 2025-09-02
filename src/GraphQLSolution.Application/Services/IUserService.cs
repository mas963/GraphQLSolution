using GraphQLSolution.Application.DTOs;

namespace GraphQLSolution.Application.Services;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(string id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<bool> DeleteUserAsync(string id);
    Task<string> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<string> LoginUserAsync(LoginUserDto loginUserDto);
}
