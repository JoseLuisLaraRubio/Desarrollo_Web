namespace GymApp.ApiService.Features.Workouts.Endpoints;

using GymApp.ApiService.Features.Routines.Services;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Workouts;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class WorkoutEndpoints
{
    public static async Task<Results<Ok<IEnumerable<WorkoutPlan>>, UnauthorizedHttpResult>> HandleGet(
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] RoutineManager routineManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        IEnumerable<WorkoutPlan> Workouts = await routineManager.GetUserWorkouts(user);

        return TypedResults.Ok(Workouts);
    }
}
