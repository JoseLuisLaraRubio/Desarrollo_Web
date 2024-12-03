namespace GymApp.ApiService.Features.Routines.Data;

using GymApp.Database.Entities.Routines;

#pragma warning disable SA1402

public record RoutineView(
    Guid Id,
    string Name,
    ICollection<WorkoutView> Workouts);

public record WorkoutView(
    Guid Id,
    ICollection<ExerciseBlockView> Blocks);

public record ExerciseBlockView(
    Guid Id,
    ExerciseView Exercise,
    int Sets,
    int Repetitions);

public record ExerciseView(
    Guid Id,
    string Name,
    Muscle PrimaryMuscle,
    ICollection<Muscle> SecondaryMuscles,
    Equipment EquipmentRequirement,
    int DifficultyLevel,
    int EffectivenessLevel);
