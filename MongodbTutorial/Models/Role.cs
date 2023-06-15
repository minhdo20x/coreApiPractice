using System.Text.Json.Serialization;

namespace MongodbTutorial.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    All,
    Standard,
    Vip,
    Admin
}