namespace GymApp.Database.Entities.Workouts;

using System.ComponentModel.DataAnnotations;

public partial class RoutineBlock
{
    public Guid Id { get; init; }

    public required Exercise Exercise { get; init; }

    [Range(1, int.MaxValue)]
    public required int Sets { get; set; }

    [Range(1, int.MaxValue)]
    public required int Repetitions { get; set; }
}
