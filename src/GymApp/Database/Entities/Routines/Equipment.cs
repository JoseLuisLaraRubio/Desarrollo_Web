namespace GymApp.Database.Entities.Routines;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Equipment
{
    None,
    Dumbbells,
    Gym,
}
