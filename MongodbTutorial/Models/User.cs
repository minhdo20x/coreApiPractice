using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongodbTutorial.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [BsonElement("firstName")]
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = null!;

    [BsonElement("lastName")]
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = null!;

    [BsonElement("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
    
    [BsonElement("phone")]
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = "+84 ";

    [BsonElement("gender")]
    [JsonPropertyName("gender")]
    public Gender? Gender { get; set; }

    [BsonElement("role")]
    [JsonPropertyName("role")]

    public Role? Role { get; set; }
    
    [BsonElement("hoppy")]
    [JsonPropertyName("hoppy")]
    public string? Hoppy { get; set; }
    
}