namespace GymApp.Database.Entities.Routines;

using System.ComponentModel.DataAnnotations;

public class Routine
{
    public Guid Id { get; init; }

    [MaxLength(7)]
    public ICollection<Workout> Workouts { get; init; } = [];
}
