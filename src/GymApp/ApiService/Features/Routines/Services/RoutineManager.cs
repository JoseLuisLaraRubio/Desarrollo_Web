namespace GymApp.ApiService.Features.Routines.Services;

using GymApp.ApiService.Features.Members.Services;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;

using Microsoft.EntityFrameworkCore;

public sealed class RoutineManager(
    MemberManager memberManager)
{
    public async Task<IReadOnlyCollection<Routine>> GetUserRoutines(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Member member = await this.QueryMember(user).FirstAsync();

        return [.. member.Routines];
    }

    private IQueryable<Member> QueryMember(AppUser user)
    {
        return memberManager.Query(user)
            .AsNoTracking()
            .Where(m => m.User == user)
            .Include(m => m.Routines)
            .ThenInclude(r => r.Days)
            .ThenInclude(d => d.Exercises);
    }
}
