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

    private Task<bool> UserOwnsWorkout(
        AppUser user,
        Guid workoutId)
    {
        return memberManager.Query(user)
            .Select(m => m.Routine!)
            .SelectMany(w => w.Workouts)
            .AnyAsync(r => r.Id == workoutId);
    }

    private async Task<ValidationResult> ValidateExerciseIds(
        WorkoutProgressDataWithId data)
    {
        IEnumerable<Guid> exerciseIds = data.Results.Select(r => r.ExerciseId);
        List<Guid> invalidExerciseIds = await exerciseManager.GetInvalidExerciseIds(exerciseIds);
        if (invalidExerciseIds.Count > 0)
        {
            IEnumerable<ValidationFailure> errors = invalidExerciseIds.Select(
                id => new ValidationFailure("ExerciseId", $"Invalid exercise id ({id})."));

            return new ValidationResult(errors);
        }

        return new ValidationResult();
    }
}
