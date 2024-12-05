namespace GymApp.ApiService.Features.Progress.Data;

using GymApp.Database.Entities.Routines.Progress;

using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class RoutineProgressViewMapper
{
    // TODO
    public static partial IEnumerable<WorkoutProgressView> EntityToView(IEnumerable<WorkoutProgress> entities);
}
