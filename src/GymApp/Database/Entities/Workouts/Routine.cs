namespace GymApp.Database.Entities.Workouts;

public class Routine
{
    public Guid Id { get; init; }

    public ICollection<RoutineBlock> Blocks { get; init; } = [];
}
