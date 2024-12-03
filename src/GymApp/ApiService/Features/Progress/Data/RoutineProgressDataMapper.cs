namespace GymApp.ApiService.Features.Progress.Data;

using GymApp.Database.Entities.Routines;
using GymApp.Database.Entities.Routines.Progress;

using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class RoutineProgressDataMapper
{
    [MapperIgnoreTarget(nameof(WorkoutProgress.Id))]
    [MapValue(nameof(WorkoutProgress.Workout), Use = nameof(NullWorkout))]
    public static partial WorkoutProgress DataToEntity(WorkoutProgressDataWithId data);

    [MapperIgnoreTarget(nameof(ExerciseBlockResult.Id))]
    [MapValue(nameof(ExerciseBlockResult.Exercise), Use = nameof(NullExercise))]
    private static partial ExerciseBlockResult DataToEntity(ExerciseBlockResultData data);

    [MapperIgnoreTarget(nameof(SetResult.Id))]
    private static partial SetResult DataToEntity(SetResultData data);

    private static Workout NullWorkout() => null!;

    private static Exercise NullExercise() => null!;
}
