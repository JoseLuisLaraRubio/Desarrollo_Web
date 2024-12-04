namespace GymApp.ApiService.Features.Routines.Services;

using FluentValidation.Results;

using GymApp.ApiService.Features.Exercises.Services;
using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Routines.Data;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;

using Microsoft.EntityFrameworkCore;

public sealed class RoutineManager(
    MemberManager memberManager,
    ExerciseManager exerciseManager)
{
    public async Task<RoutineView?> GetUserRoutine(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Routine? routine = await this.QueryRoutine(user, QueryTrackingBehavior.NoTracking).FirstOrDefaultAsync();

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

    public async Task<ValidationResult> UpdateRoutine(AppUser user, RoutineUpdate update)
    {
        ArgumentNullException.ThrowIfNull(user);

        var exerciseIds = update.Workouts.SelectMany(w => w.Blocks).Select(b => b.ExerciseId);
        if (await exerciseManager.ValidateExerciseIds(exerciseIds) is { IsValid: false } result)
        {
            return result;
        }

        Routine? routine = await this.QueryRoutine(user, QueryTrackingBehavior.TrackAll).FirstOrDefaultAsync();
        if (routine == null)
        {
            var error = new ValidationFailure("Routine", "User does not have a routine.");
            return new ValidationResult([error]);
        }

        update.Apply(routine);
        await memberManager.SaveChanges();

        return new ValidationResult();
    }

    private IQueryable<Routine?> QueryRoutine(
        AppUser user,
        QueryTrackingBehavior trackingBehavior)
    {
        return memberManager.Query(user)
            .AsTracking(trackingBehavior)
            .Include(m => m.Routine!)
            .ThenInclude(r => r.Workouts)
            .ThenInclude(w => w.Blocks)
            .ThenInclude(b => b.Exercise)
            .Select(m => m.Routine);
    }
}
