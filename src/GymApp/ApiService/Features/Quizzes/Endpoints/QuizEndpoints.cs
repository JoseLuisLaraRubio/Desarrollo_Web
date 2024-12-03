namespace GymApp.ApiService.Features.Quizzes.Endpoints;

using GymApp.ApiService.Features.Exercises.Services;
using GymApp.ApiService.Features.Quizzes.Data;
using GymApp.ApiService.Features.Workouts.Services;
using GymApp.Database.Entities;
using GymApp.Database.Entities.Workouts;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class QuizEndpoints
{
    public static Ok<Quiz> HandleGet()
    {
        return TypedResults.Ok(RoutineQuiz.Instance);
    }

    public static async Task<Results<Ok, BadRequest<string>, UnauthorizedHttpResult>> HandlePost(
        [FromBody] QuizResponse quizResponse,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] WorkoutManager workoutManager,
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
        WorkoutPlan newWorkout = await GenerateWorkout(exerciseManager);
        await workoutManager.AddWorkout(user, newWorkout);

        return TypedResults.Ok();
    }

    // TODO: Move to service?
    private static async Task<WorkoutPlan> GenerateWorkout(ExerciseManager exerciseManager)
    {
        IReadOnlyCollection<Exercise> exercises = await exerciseManager.GetExercises();
        var exercise1 = exercises.ElementAt(0);
        var exercise2 = exercises.ElementAt(1);

        return new WorkoutPlan()
        {
            Routines =
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
