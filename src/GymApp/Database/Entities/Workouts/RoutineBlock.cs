namespace GymApp.Database.Entities.Workouts;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public partial class RoutineBlock
{
    public Guid Id { get; init; }

    [JsonIgnore]
    public required Exercise Exercise { get; init; }

    [Range(1, int.MaxValue)]
    public int Sets { get; set; }

    [Range(1, int.MaxValue)]
    public int Repetitions { get; set; }
}
