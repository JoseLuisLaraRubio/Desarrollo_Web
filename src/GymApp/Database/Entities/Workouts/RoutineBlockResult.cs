namespace GymApp.Database.Entities.Workouts;

public class RoutineBlockResult
{
    public Guid Id { get; init; }

    public required Exercise Exercise { get; init; }

    public ICollection<SetResult> Sets { get; init; } = [];
}
