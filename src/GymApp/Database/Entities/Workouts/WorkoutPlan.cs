namespace GymApp.Database.Entities.Workouts;

using System.ComponentModel.DataAnnotations;

public class WorkoutPlan
{
    public Guid Id { get; init; }

    [MaxLength(7)]
    public ICollection<Routine> Routines { get; init; } = [];
}
