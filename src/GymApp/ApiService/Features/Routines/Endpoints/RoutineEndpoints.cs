namespace GymApp.ApiService.Features.Routines.Endpoints;

using GymApp.ApiService.Features.Routines.Data;
using GymApp.ApiService.Features.Routines.Services;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class RoutineEndpoints
{
    public static async Task<Results<Ok<RoutineView>, NoContent, UnauthorizedHttpResult>> HandleGet(
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] RoutineManager routineManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        RoutineView? routine = await routineManager.GetUserRoutine(user);

        return routine is null
            ? TypedResults.NoContent()
            : TypedResults.Ok(routine);
    }

    public static async Task<Results<Ok, ValidationProblem, UnauthorizedHttpResult>> HandlePut(
        [FromBody] RoutineUpdate routineUpdate,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] RoutineManager routineManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        var result = await routineManager.UpdateRoutine(user, routineUpdate);

        return !result.IsValid
            ? TypedResults.ValidationProblem(result.ToDictionary())
            : TypedResults.Ok();
    }
}
