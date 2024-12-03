namespace GymApp.Database.Entities.Routines.Progress;

public class SetResult
{
    public int Id { get; init; }

    public required float Weight { get; init; }

    public required int Repetitions { get; init; }
}
