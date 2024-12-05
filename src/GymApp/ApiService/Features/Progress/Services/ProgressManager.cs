namespace GymApp.ApiService.Features.Progress.Services;

using FluentValidation.Results;

using GymApp.ApiService.Features.Exercises.Services;
using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Progress.Data;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines.Progress;

using Microsoft.EntityFrameworkCore;

public sealed class ProgressManager(
    MemberManager memberManager,
    ExerciseManager exerciseManager)
{
    public async Task<IEnumerable<WorkoutProgressView>?> GetRoutineProgress(AppUser user, Guid workoutId)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!await this.UserOwnsWorkout(user, workoutId))
        {
            return null;
        }

        var progress = await this.QueryProgress(user, workoutId).ToListAsync();

        return progress == null
            ? null
            : RoutineProgressViewMapper.EntityToView(progress);
    }

    public async Task<ValidationResult> AddRoutineProgress(
        AppUser user,
        WorkoutProgressDataWithId data)
    {
        if (!await this.UserOwnsWorkout(user, data.WorkoutId))
        {
            var error = new ValidationFailure("WorkoutId", "Invalid workout id.");
            return new ValidationResult([error]);
        }

        if (await this.ValidateExerciseIds(data) is { IsValid: false } result)
        {
            return result;
        }

        WorkoutProgress progress = RoutineProgressDataMapper.DataToEntity(data);

        Member member = memberManager.Attach(user);
        member.Progress.Add(progress);

        await memberManager.SaveChanges();

        return new ValidationResult();
    }

    private IQueryable<WorkoutProgress> QueryProgress(AppUser user, Guid workoutId)
    {
        return memberManager.Query(user)
            .AsNoTracking()
            .SelectMany(m => m.Progress)
            .Where(p => p.Workout.Id == workoutId)
            .Include(p => p.Results)
            .ThenInclude(r => r.Exercise)
            .Include(p => p.Results)
            .ThenInclude(r => r.Sets);
    }

    private Task<bool> UserOwnsWorkout(
        AppUser user,
        Guid workoutId)
    {
        return memberManager.Query(user)
            .Select(m => m.Routine!)
            .SelectMany(w => w.Workouts)
            .AnyAsync(r => r.Id == workoutId);
    }

    private Task<ValidationResult> ValidateExerciseIds(
        WorkoutProgressDataWithId data)
    {
        IEnumerable<Guid> exerciseIds = data.Results.Select(r => r.ExerciseId);

        return exerciseManager.ValidateExerciseIds(exerciseIds);
    }
}
