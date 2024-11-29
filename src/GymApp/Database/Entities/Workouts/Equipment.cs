namespace GymApp.Database.Entities.Workouts;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Equipment
{
    None,
    Dumbbells,
    Gym,
}
