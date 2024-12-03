namespace GymApp.ApiService.Features.Routines.Data;

using GymApp.Database.Entities.Routines;

using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class RoutineDataMapper
{
    public static partial RoutineView RoutineToView(Routine routine);

    public static partial WorkoutView WorkoutToView(Workout workout);

    [MapperIgnoreSource(nameof(ExerciseBlock.ExerciseId))]
    private static partial ExerciseBlockView BlockToView(ExerciseBlock block);
}
