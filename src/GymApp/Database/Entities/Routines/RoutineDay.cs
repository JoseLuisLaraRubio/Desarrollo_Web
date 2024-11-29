namespace GymApp.Database.Entities.Routines;

public class RoutineDay
{
    public Guid Id { get; init; }

    public required DayOfWeek DayOfWeek { get; init; }

    public ICollection<ExerciseInRoutine> Exercises { get; init; } = [];
}
