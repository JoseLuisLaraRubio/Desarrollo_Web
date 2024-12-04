namespace GymApp.ApiService.Features.Quizzes.Endpoints;

using GymApp.ApiService.Features.Exercises.Services;
using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.Quizzes.Data;
using GymApp.ApiService.Features.Routines.Services;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RaptorUtils.AspNet.Identity;

public static class QuizEndpoints
{
    public static async Task<Results<Ok<bool>, UnauthorizedHttpResult>> HandleGetStatus(
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] MemberManager memberManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        bool status = await memberManager
            .Query(user)
            .AsNoTracking()
            .Select(m => m.QuizStatus)
            .FirstAsync();

        return TypedResults.Ok(status);
    }

    public static Ok<Quiz> HandleGet()
    {
        return TypedResults.Ok(RoutineQuiz.Instance);
    }

    public static async Task<Results<Ok, BadRequest<string>, UnauthorizedHttpResult>> HandlePost(
        [FromBody] QuizResponse quizResponse,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] RoutineManager routineManager,
        [FromServices] ExerciseManager exerciseManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        if (quizResponse.AnswersIndices.Count != RoutineQuiz.Instance.Questions.Count)
        {
            return TypedResults.BadRequest("Indices count must match quiz questions count.");
        }

        // TODO
        Routine newRoutine = await GenerateRoutine(exerciseManager);
        await routineManager.SetRoutine(user, newRoutine);

        return TypedResults.Ok();
    }

    // TODO: Move to service?
    private static async Task<Routine> GenerateRoutine(ExerciseManager exerciseManager)
    {
        IReadOnlyCollection<Exercise> exercises = await exerciseManager.GetExercises();
        Exercise exercise1 = exercises.ElementAt(0);
        Exercise exercise2 = exercises.ElementAt(1);

        return new Routine()
        {
            Name = "Default routine",
            Workouts =
            [
                new()
                {
                    Blocks =
                    [
                        new()
                        {
                            Exercise = exercise1,
                            Repetitions = 5,
                            Sets = 3,
                        },
                    ],
                },
                new()
                {
                    Blocks =
                    [
                        new()
                        {
                            Exercise = exercise2,
                            Repetitions = 8,
                            Sets = 4,
                        },
                        new()
                        {
                            Exercise = exercise1,
                            Repetitions = 7,
                            Sets = 5,
                        },
                    ],
                }
            ],
        };
    }
}
