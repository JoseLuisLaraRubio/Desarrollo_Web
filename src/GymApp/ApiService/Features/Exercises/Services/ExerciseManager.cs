namespace GymApp.ApiService.Features.Exercises.Services;

using GymApp.Database;
using GymApp.Database.Entities.Workouts;

using Microsoft.EntityFrameworkCore;

public sealed class ExerciseManager(
    AppDbContext dbContext)
{
    public async Task<IReadOnlyCollection<Exercise>> GetExercises()
    {
        return await this.GetExerciseSet()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Guid>> GetInvalidExerciseIds(IEnumerable<Guid> ids)
    {
        List<Guid> allIds = await this.GetExerciseSet().Select(e => e.Id).ToListAsync();

        return ids.Except(allIds).ToList();
    }

    private DbSet<Exercise> GetExerciseSet() => dbContext.Set<Exercise>();
}
