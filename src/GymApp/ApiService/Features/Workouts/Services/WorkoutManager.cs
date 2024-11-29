namespace GymApp.ApiService.Features.Workouts.Services;

using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Workouts.Data;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Workouts;

using Microsoft.EntityFrameworkCore;

public sealed class WorkoutManager(
    MemberManager memberManager)
{
    public async Task<IReadOnlyCollection<WorkoutView>> GetUserWorkouts(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        ICollection<WorkoutPlan> workouts = await this.QueryWorkouts(user).FirstAsync();

        return [.. WorkoutDataMapper.WorkoutToView(workouts)];
    }

    public async Task AddWorkout(AppUser user, WorkoutPlan workout)
    {
        ArgumentNullException.ThrowIfNull(user);

        Member member = await memberManager.Query(user).FirstAsync();
        member.Workouts.Add(workout);

        await memberManager.SaveChanges();
    }

    private IQueryable<ICollection<WorkoutPlan>> QueryWorkouts(AppUser user)
    {
        return memberManager.Query(user)
            .AsNoTracking()
            .Include(m => m.Workouts)
            .ThenInclude(r => r.Routines)
            .ThenInclude(d => d.Blocks)
            .Select(m => m.Workouts);
    }
}
