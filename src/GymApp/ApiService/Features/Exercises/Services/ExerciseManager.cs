namespace GymApp.ApiService.Features.Exercises.Services;

using System.Diagnostics.Contracts;

using FluentValidation.Results;

using GymApp.Database;
using GymApp.Database.Entities.Routines;

using Microsoft.EntityFrameworkCore;

public sealed class ExerciseManager(
    AppDbContext dbContext)
{
    [Pure]
    public async Task<IReadOnlyCollection<Exercise>> GetExercises()
    {
        return await this.GetExerciseSet()
            .AsNoTracking()
            .ToListAsync();
    }

    [Pure]
    public async Task<ValidationResult> ValidateExerciseIds(IEnumerable<Guid> ids)
    {
        List<Guid> invalidExerciseIds = await this.GetInvalidExerciseIds(ids);
        if (invalidExerciseIds.Count > 0)
        {
            IEnumerable<ValidationFailure> errors = invalidExerciseIds.Select(
                id => new ValidationFailure("ExerciseId", $"Invalid exercise id ({id})."));

            return new ValidationResult(errors);
        }

        return new ValidationResult();
    }

    [Pure]
    private async Task<List<Guid>> GetInvalidExerciseIds(IEnumerable<Guid> ids)
    {
        List<Guid> allIds = await this.GetExerciseSet().Select(e => e.Id).ToListAsync();

        return ids.Except(allIds).ToList();
    }

    [Pure]
    private DbSet<Exercise> GetExerciseSet() => dbContext.Set<Exercise>();
}
