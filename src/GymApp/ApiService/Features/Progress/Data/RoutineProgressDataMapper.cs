namespace GymApp.ApiService.Features.Progress.Data;

using GymApp.Database.Entities.Workouts;

using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class RoutineProgressDataMapper
{
    [MapValue(nameof(RoutineProgress.Id), Use = nameof(EmptyId))]
    [MapValue(nameof(RoutineProgress.Routine), Use = nameof(IgnoreRoutine))]
    public static partial RoutineProgress DataToEntity(RoutineProgressDataWithId data);

    [MapValue(nameof(RoutineBlockResult.Id), Use = nameof(EmptyId))]
    [MapValue(nameof(RoutineBlockResult.Exercise), Use = nameof(IgnoreExercise))]
    private static partial RoutineBlockResult DataToEntity(RoutineBlockResultData data);

    [MapValue(nameof(SetResult.Id), 0)]
    private static partial SetResult DataToEntity(SetResultData data);

    private static Guid EmptyId() => Guid.Empty;

    private static Routine IgnoreRoutine() => null!;

    private static Exercise IgnoreExercise() => null!;
}
