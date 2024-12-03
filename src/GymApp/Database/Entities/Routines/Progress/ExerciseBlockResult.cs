namespace GymApp.Database.Entities.Routines.Progress;

public partial class ExerciseBlockResult
{
    public Guid Id { get; init; }

    public required Exercise Exercise { get; init; }

    public ICollection<SetResult> Sets { get; init; } = [];
}
