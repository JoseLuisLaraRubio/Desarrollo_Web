namespace GymApp.ApiService.Features.Progress.Endpoints;

using FluentValidation.Results;

using GymApp.ApiService.Features.Progress.Data;
using GymApp.ApiService.Features.Progress.Services;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class ProgressEndpoints
{
    public static async Task<Results<Ok<IEnumerable<WorkoutProgressView>>, ValidationProblem, UnauthorizedHttpResult>> HandleGet(
        [FromRoute] Guid workoutId,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] ProgressManager progressManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        var progress = await progressManager.GetRoutineProgress(user, workoutId);

        if (progress == null)
        {
            var error = new ValidationFailure("WorkoutId", "Invalid workout id.");
            var result = new ValidationResult([error]);
            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        return TypedResults.Ok(progress);
    }

    public static async Task<Results<Ok, ValidationProblem, UnauthorizedHttpResult>> HandlePost(
        [FromRoute] Guid workoutId,
        [FromBody] WorkoutProgressData routineProgressData,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] ProgressManager progressManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        var result = await progressManager.AddRoutineProgress(user, routineProgressData.WithId(workoutId));

        return !result.IsValid
            ? TypedResults.ValidationProblem(result.ToDictionary())
            : TypedResults.Ok();
    }
}
