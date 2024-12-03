namespace GymApp.ApiService.Features.Workouts.Endpoints;

using GymApp.ApiService.Features.Routines.Data;
using GymApp.ApiService.Features.Workouts.Services;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class WorkoutEndpoints
{
    public static async Task<Results<Ok<WorkoutView>, NotFound, UnauthorizedHttpResult>> HandleGet(
        [FromRoute] Guid workoutId,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] WorkoutManager workoutManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        WorkoutView? workout = await workoutManager.GetUserWorkoutById(user, workoutId);

        return workout is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(workout);
    }
}
