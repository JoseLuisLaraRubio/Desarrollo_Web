namespace GymApp.ApiService.Features.Progress.Services;

using FluentValidation.Results;

using GymApp.ApiService.Features.Exercises.Services;
using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Progress.Data;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Workouts;

using Microsoft.EntityFrameworkCore;

public sealed class ProgressManager(
    MemberManager memberManager,
    ExerciseManager exerciseManager)
{
    public async Task<ValidationResult> AddRoutineProgress(
        AppUser user,
        RoutineProgressDataWithId data)
    {
        if (!await this.UserOwnsRoutine(user, data.RoutineId))
        {
            var error = new ValidationFailure("RoutineId", "Invalid routine id.");
            return new ValidationResult([error]);
        }

        if (await this.ValidateExerciseIds(data) is { IsValid: false } result)
        {
            return result;
        }

        RoutineProgress progress = RoutineProgressDataMapper.DataToEntity(data);

        Member member = memberManager.Attach(user);
        member.Progress.Add(progress);

        await memberManager.SaveChanges();

        return new ValidationResult();
    }

    private Task<bool> UserOwnsRoutine(
        AppUser user,
        Guid routineId)
    {
        return memberManager.Query(user)
            .SelectMany(m => m.Workouts)
            .SelectMany(w => w.Routines)
            .AnyAsync(r => r.Id == routineId);
    }

    private async Task<ValidationResult> ValidateExerciseIds(
        RoutineProgressDataWithId data)
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
