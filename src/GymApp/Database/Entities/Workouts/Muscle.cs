namespace GymApp.Database.Entities.Workouts;

using System.Text.Json.Serialization;

// TODO: Set values
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Muscle
{
    Chest,
    Back,
    Legs,
    Arms,
    Shoulders,
    Abs,
    Glutes,
}
