using HotChocolate.Authorization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQLSolution.Domain.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    [Authorize]
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}