namespace GymApp.ApiService.Features.Quizzes.Endpoints;

using System.Diagnostics;
using System.Reflection.Metadata;

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
using RaptorUtils.Net;

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

        var userPreferences = CalculatePreferences(quizResponse);
        Routine newRoutine = await GenerateRoutine(exerciseManager, userPreferences);
        await routineManager.SetRoutine(user, newRoutine);

        return TypedResults.Ok();
    }

    private static async Task<Routine> GenerateRoutine(ExerciseManager exerciseManager, Preferences preferences)
    {
        IReadOnlyCollection<Exercise> exercises = await exerciseManager.GetExercises();

        var filteredExercises = exercises
            .Where(ex => ex.DifficultyLevel <= preferences.MaxDifficulty &&
            (int)ex.EquipmentRequirement <= preferences.Equipment).ToList();

        int totalSets = preferences.ExercisesPerSesion * preferences.WeekDays;

        // Crear una lista para almacenar los bloques de la rutina
        List<ExerciseBlock> blocks = new List<ExerciseBlock>();

        // Calcular cuántos sets asignar a cada grupo muscular
        Dictionary<Muscle, float> muscleExercises = new Dictionary<Muscle, float>();
        muscleExercises.Add(Muscle.MidBack, 0);
        muscleExercises.Add(Muscle.Lats, 0);
        muscleExercises.Add(Muscle.Chest, 0);
        muscleExercises.Add(Muscle.Quads, 0);
        muscleExercises.Add(Muscle.Glutes, 0);
        muscleExercises.Add(Muscle.Hamstrings, 0);
        muscleExercises.Add(Muscle.Calves, 0);
        muscleExercises.Add(Muscle.Biceps, 0);
        muscleExercises.Add(Muscle.Triceps, 0);
        muscleExercises.Add(Muscle.Shoulders, 0);
        muscleExercises.Add(Muscle.Abs, 0);

        int setsPerMuscle = totalSets / 10;

        foreach (var muscle in muscleExercises.Keys.ToList())
        {
            // Find all exercises targeting the current muscle group
            var exercisesForMuscle = filteredExercises.FindAll(ex => ex.PrimaryMuscle == muscle);

            if (exercisesForMuscle.Count > 0)
            {
                int i = 0;
                while (muscleExercises[muscle] < setsPerMuscle)
                {
                    // Assign the exercise to the routine
                    blocks.Add(new ExerciseBlock
                    {
                        Exercise = exercisesForMuscle[i],
                        Sets = preferences.Sets,
                        Repetitions = preferences.Reps,
                    });

                    muscleExercises[muscle]++;
                    foreach (var secMuscle in exercisesForMuscle[i].SecondaryMuscles)
                    {
                        muscleExercises[secMuscle] += .5f;
                    }

                    i++;
                    if (i >= exercisesForMuscle.Count)
                    {
                        i = 0;
                    }
                }
            }
        }

        var workouts = new Workout[preferences.WeekDays];
        for (int i = 0; i < workouts.Length; i++)
        {
            workouts[i] = new Workout { Blocks = new List<ExerciseBlock>() };
        }

        int j = 0;
        foreach (var block in blocks)
        {
            workouts[j].Blocks.Add(block);
            j++;
            if (j >= workouts.Length)
            {
                j = 0;
            }
        }

        return new Routine()
        {
            Name = "Rutina Inicial",
            Workouts = workouts,
        };
    }

    private static Preferences CalculatePreferences(QuizResponse response)
    {
        // ¿Cuántos días a la semana puedes entrenar?
        var diasSemana = response.AnswersIndices.ElementAt(0) switch
        {
            0 => 4,
            1 => 5,
            2 => 6,
            _ => 5,
        };

        // ¿Cuánto tiempo puedes dedicar a cada sesión de entrenamiento?
        var ejerciciosPorSesion = response.AnswersIndices.ElementAt(1) switch
        {
            0 => 4,
            1 => 6,
            2 => 8,
            _ => 6,
        };

        // ¿Cuál es tu nivel actual de experiencia en entrenamiento?
        var maxDificultad = response.AnswersIndices.ElementAt(2) switch
        {
            0 => 5,
            1 => 8,
            2 => 10,
            _ => 10,
        };

        // ¿Qué tipo de equipo tienes disponible para entrenar?
        var equipo = response.AnswersIndices.ElementAt(3) switch
        {
            0 => 3,
            1 => 2,
            2 => 1,
            _ => 3,
        };

        // ¿Que tan intensamente te gustaría entrenar?
        var sets = response.AnswersIndices.ElementAt(4) switch
        {
            0 => 2,
            1 => 3,
            2 => 4,
            _ => 3,
        };

        // ¿Qué tan exigente te gustaría que fuera tu entrenamiento?
        _ = response.AnswersIndices.ElementAt(5) switch
        {
            0 => sets++,
            1 => sets,
            2 => sets--,
            _ => 3,
        };

        // lo siento mucho
        if (sets == 1)
        {
            sets++;
        }

        // ¿Que rango de repeticiones prefieres?
        var reps = response.AnswersIndices.ElementAt(6) switch
        {
            0 => 15,
            1 => 10,
            2 => 6,
            _ => 10,
        };

        return new Preferences(
            diasSemana,
            ejerciciosPorSesion,
            maxDificultad,
            equipo,
            sets,
            reps);
    }
}
