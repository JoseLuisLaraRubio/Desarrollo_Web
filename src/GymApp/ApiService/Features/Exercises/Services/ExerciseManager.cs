namespace GymApp.ApiService.Features.Exercises.Services;

using GymApp.Database;
using GymApp.Database.Entities.Routines;

using Microsoft.EntityFrameworkCore;

public sealed class ExerciseManager(
    AppDbContext dbContext)
{
    public async Task<IReadOnlyCollection<Exercise>> GetExercises(QueryTrackingBehavior trackingBehavior)
    {
        return await dbContext.Set<Exercise>()
            .AsTracking(trackingBehavior)
            .ToListAsync();
    }
}
