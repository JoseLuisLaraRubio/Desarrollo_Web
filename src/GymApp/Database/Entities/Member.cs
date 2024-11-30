namespace GymApp.Database.Entities;

using GymApp.Database.Entities.Workouts;

public class Member
{
    public required AppUser User { get; init; }

    public ICollection<WorkoutPlan> Workouts { get; init; } = [];

    public ICollection<RoutineProgress> Progress { get; init; } = [];
}
