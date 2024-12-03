namespace GymApp.ApiService.Features.Routines.Services;

using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Routines.Data;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;

using Microsoft.EntityFrameworkCore;

public sealed class RoutineManager(
    MemberManager memberManager)
{
    public async Task<RoutineView?> GetUserRoutine(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Routine? routine = await this.QueryRoutine(user).FirstOrDefaultAsync();

        return routine == null
            ? null
            : RoutineDataMapper.RoutineToView(routine);
    }

    public async Task SetRoutine(AppUser user, Routine routine)
    {
        ArgumentNullException.ThrowIfNull(user);

        Member member = memberManager.Attach(user);
        member.Routine = routine;

        await memberManager.SaveChanges();
    }

    private IQueryable<Routine?> QueryRoutine(AppUser user)
    {
        return memberManager.Query(user)
            .AsNoTracking()
            .Include(m => m.Routine!)
            .ThenInclude(r => r.Workouts)
            .ThenInclude(w => w.Blocks)
            .ThenInclude(b => b.Exercise)
            .Select(m => m.Routine);
    }
}
