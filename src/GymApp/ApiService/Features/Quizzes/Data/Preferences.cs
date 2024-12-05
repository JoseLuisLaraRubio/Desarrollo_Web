namespace GymApp.ApiService.Features.Quizzes.Data;

public record Preferences(
    int WeekDays,
    int ExercisesPerSesion,
    int MaxDifficulty,
    int Equipment,
    int Sets,
    int Reps);
