namespace GymApp.ApiService.Features.Workouts.Data;

using GymApp.Database.Entities.Workouts;

using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class WorkoutDataMapper
{
    public static partial IEnumerable<WorkoutView> WorkoutToView(IEnumerable<WorkoutPlan> workout);

    [MapperIgnoreSource(nameof(RoutineBlock.Exercise))]
    private static partial RoutineBlockView BlockToView(RoutineBlock block);
}
