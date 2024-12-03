namespace GymApp.Database.Entities.Routines.Progress;

public partial class WorkoutProgress
{
    public Guid Id { get; init; }

    public required Workout Workout { get; init; }

    public required DateTimeOffset Date { get; init; }

    public ICollection<ExerciseBlockResult> Results { get; init; } = [];
}
