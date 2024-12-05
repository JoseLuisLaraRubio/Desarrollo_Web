namespace GymApp.Database.Entities.Routines;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Muscle
{
    MidBack,
    Lats,
    Chest,
    Quads,
    Glutes,
    Hamstrings,
    Calves,
    Biceps,
    Triceps,
    Shoulders,
    Abs,
}
