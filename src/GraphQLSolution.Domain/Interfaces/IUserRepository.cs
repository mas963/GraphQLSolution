using GraphQLSolution.Domain.Entities;

namespace GraphQLSolution.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}
