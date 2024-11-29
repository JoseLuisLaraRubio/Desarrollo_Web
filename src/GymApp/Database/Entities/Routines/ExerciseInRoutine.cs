namespace GymApp.Database.Entities.Routines;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ExerciseInRoutine
{
    public Guid Id { get; init; }

    public Guid ExerciseId { get; init; }

    [JsonIgnore]
    public required Exercise Exercise { get; init; }

    [Range(1, int.MaxValue)]
    public int Sets { get; set; }

    [Range(1, int.MaxValue)]
    public int Repetitions { get; set; }
}
