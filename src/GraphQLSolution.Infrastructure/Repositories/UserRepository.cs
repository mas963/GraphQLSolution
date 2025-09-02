using GraphQLSolution.Domain.Entities;
using GraphQLSolution.Domain.Interfaces;
using GraphQLSolution.Infrastructure.Data;
using MongoDB.Driver;

namespace GraphQLSolution.Infrastructure.Repositories;

public class UserRepository : MongoRepository<User>, IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(MongoContext context) : base(context, "users")
    {
        _collection = context.GetCollection<User>("users");
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }
}
