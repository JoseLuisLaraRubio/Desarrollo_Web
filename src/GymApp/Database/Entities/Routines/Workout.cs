namespace GymApp.Database.Entities.Routines;

public class Workout
{
    public Guid Id { get; init; }

    public ICollection<ExerciseBlock> Blocks { get; init; } = [];
}
