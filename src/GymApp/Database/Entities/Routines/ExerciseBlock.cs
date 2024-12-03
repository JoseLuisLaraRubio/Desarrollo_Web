namespace GymApp.Database.Entities.Routines;

using System.ComponentModel.DataAnnotations;

public partial class ExerciseBlock
{
    public Guid Id { get; init; }

    public required Exercise Exercise { get; init; }

    [Range(1, int.MaxValue)]
    public required int Sets { get; set; }

    [Range(1, int.MaxValue)]
    public required int Repetitions { get; set; }
}
