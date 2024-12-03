namespace GymApp.ApiService.Features.Exercises.Endpoints;

using GymApp.ApiService.Features.Exercises.Services;
using GymApp.Database.Entities.Routines;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public static class ExerciseEndpoints
{
    public static async Task<Ok<IEnumerable<Exercise>>> HandleGet(
        [FromServices] ExerciseManager exerciseManager)
    {
        IEnumerable<Exercise> exercises = await exerciseManager.GetExercises();
        return TypedResults.Ok(exercises);
    }
}
