using GraphQLSolution.Application.DTOs;
using GraphQLSolution.Application.Exceptions;
using GraphQLSolution.Domain.Entities;
using GraphQLSolution.Domain.Interfaces;

namespace GraphQLSolution.Application.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public UserService(IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserDto(u.Id, u.Email, u.FullName, u.CreatedAt, u.IsActive));
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return new UserDto(user.Id, user.Email, user.FullName, user.CreatedAt, user.IsActive);
    }

    public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginUserDto.Email);

        if (user == null || !_passwordService.VerifyPassword(loginUserDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        return _jwtService.GenerateToken(user.Id, user.Email, user.Role);
    }

    public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var userExists = await _userRepository.EmailExistsAsync(registerUserDto.Email);

        if (userExists)
        {
            throw new ConflictException("User already exists");
        }

        var user = new User
        {
            Email = registerUserDto.Email,
            FullName = registerUserDto.FullName,
            PasswordHash = _passwordService.HashPassword(registerUserDto.Password),
        };

        await _userRepository.CreateAsync(user);

        return _jwtService.GenerateToken(user.Id, user.Email, user.Role);
    }
}
