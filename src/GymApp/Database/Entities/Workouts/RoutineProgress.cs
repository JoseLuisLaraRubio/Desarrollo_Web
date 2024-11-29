namespace GymApp.Database.Entities.Workouts;

public class RoutineProgress
{
    public Guid Id { get; init; }

    public required Routine Routine { get; init; }

    public required DateTimeOffset Date { get; init; }

    public ICollection<RoutineBlockResult> Results { get; init; } = [];
}
