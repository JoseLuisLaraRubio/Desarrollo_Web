namespace GymApp.ApiService.Features.Workouts.Endpoints;

using GymApp.ApiService.Features.Workouts.Data;
using GymApp.ApiService.Features.Workouts.Services;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class WorkoutEndpoints
{
    public static async Task<Results<Ok<IEnumerable<WorkoutView>>, UnauthorizedHttpResult>> HandleGet(
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] WorkoutManager workoutManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        IEnumerable<WorkoutView> workouts = await workoutManager.GetUserWorkouts(user);

        return TypedResults.Ok(workouts);
    }
}
