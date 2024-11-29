namespace GymApp.ApiService.Features.Routines.Services;

using GymApp.ApiService.Features.Members.Services;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Workouts;

using Microsoft.EntityFrameworkCore;

public sealed class RoutineManager(
    MemberManager memberManager)
{
    public async Task<IReadOnlyCollection<WorkoutPlan>> GetUserWorkouts(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Member member = await this.QueryMember(user).FirstAsync();

        return [.. member.Workouts];
    }

    private IQueryable<Member> QueryMember(AppUser user)
    {
        return memberManager.Query(user)
            .AsNoTracking()
            .Where(m => m.User == user)
            .Include(m => m.Workouts)
            .ThenInclude(r => r.Routines)
            .ThenInclude(d => d.Blocks);
    }
}
