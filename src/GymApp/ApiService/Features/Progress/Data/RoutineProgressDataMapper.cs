namespace GymApp.ApiService.Features.Progress.Data;

using GymApp.Database.Entities.Workouts;

using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class RoutineProgressDataMapper
{
    [MapperIgnoreTarget(nameof(RoutineProgress.Id))]
    [MapValue(nameof(RoutineProgress.Routine), Use = nameof(NullRoutine))]
    public static partial RoutineProgress DataToEntity(RoutineProgressDataWithId data);

    [MapperIgnoreTarget(nameof(RoutineBlockResult.Id))]
    [MapValue(nameof(RoutineBlockResult.Exercise), Use = nameof(NullExercise))]
    private static partial RoutineBlockResult DataToEntity(RoutineBlockResultData data);

    [MapperIgnoreTarget(nameof(SetResult.Id))]
    private static partial SetResult DataToEntity(SetResultData data);

    private static Routine NullRoutine() => null!;

    private static Exercise NullExercise() => null!;
}
