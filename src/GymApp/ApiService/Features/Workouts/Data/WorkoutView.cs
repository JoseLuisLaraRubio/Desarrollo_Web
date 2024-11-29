namespace GymApp.ApiService.Features.Workouts.Data;

#pragma warning disable SA1402

public record WorkoutView(
    Guid Id,
    ICollection<RoutineView> Routines);

public record RoutineView(
    Guid Id,
    ICollection<RoutineBlockView> Blocks);

public record RoutineBlockView(
    Guid Id,
    Guid ExerciseId,
    int Sets,
    int Repetitions);
