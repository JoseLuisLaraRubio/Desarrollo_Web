namespace GymApp.ApiService.Features.Workouts.Services;

using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Routines.Data;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;

using Microsoft.EntityFrameworkCore;

public sealed class WorkoutManager(
    MemberManager memberManager)
{
    public async Task<WorkoutView?> GetUserWorkoutById(AppUser user, Guid id)
    {
        ArgumentNullException.ThrowIfNull(user);

        Workout? workout = await this.QueryWorkoutById(user, id).FirstOrDefaultAsync();

        return workout == null
            ? null
            : RoutineDataMapper.WorkoutToView(workout);
    }

    private IQueryable<Workout?> QueryWorkoutById(AppUser user, Guid id)
    {
        return memberManager.Query(user)
            .AsNoTracking()
            .Include(m => m.Routine!)
            .ThenInclude(r => r.Workouts)
            .ThenInclude(w => w.Blocks)
            .ThenInclude(b => b.Exercise)
            .Select(m => m.Routine!)
            .SelectMany(r => r.Workouts)
            .Where(w => w.Id == id);
    }
}
