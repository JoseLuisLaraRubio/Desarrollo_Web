namespace GymApp.ApiService.Features.Progress.Endpoints;

using GymApp.ApiService.Features.Progress.Data;
using GymApp.ApiService.Features.Progress.Services;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class ProgressEndpoints
{
    public static async Task<Results<Ok, ValidationProblem, UnauthorizedHttpResult>> HandlePost(
        [FromRoute] Guid routineId,
        [FromBody] RoutineProgressData routineProgressData,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] ProgressManager progressManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        var result = await progressManager.AddRoutineProgress(user, routineProgressData.WithId(routineId));
        if (!result.IsValid)
        {
            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        return TypedResults.Ok();
    }
}
