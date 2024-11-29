namespace GymApp.ApiService.Features.Quizzes.Endpoints;

using GymApp.ApiService.Features.Quizzes.Data;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class QuizEndpoints
{
    public static Ok<Quiz> HandleGet()
    {
        return TypedResults.Ok(RoutineQuiz.Instance);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult>> HandlePost(
        [FromBody] QuizResponse quizResponse,
        [FromServices] UserContext<AppUser> userContext)
    {
        // TODO
        return TypedResults.Ok();
    }
}
