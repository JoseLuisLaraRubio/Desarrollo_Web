namespace GymApp.Database.Entities.Routines;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Equipment
{
    None = 1,
    Dumbbells = 2,
    Gym = 3,
}
