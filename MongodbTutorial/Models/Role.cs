using System.Text.Json.Serialization;

namespace MongodbTutorial.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Standard,
    Vip,
    Admin
}