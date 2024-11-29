namespace GymApp.ApiService.Features.Routines.Endpoints;

using GymApp.ApiService.Features.Routines.Services;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class RoutineEndpoints
{
    public static async Task<Results<Ok<IEnumerable<Routine>>, UnauthorizedHttpResult>> HandleGet(
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] RoutineManager routineManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        IEnumerable<Routine> routines = await routineManager.GetUserRoutines(user);

        return TypedResults.Ok(routines);
    }
}
