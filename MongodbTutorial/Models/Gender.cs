using System.Text.Json.Serialization;

namespace MongodbTutorial.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    All,
    Other,
    Male,
    Female,
}