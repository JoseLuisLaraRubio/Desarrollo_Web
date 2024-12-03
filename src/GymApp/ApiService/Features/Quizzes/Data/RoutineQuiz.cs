namespace GymApp.ApiService.Features.Quizzes.Data;

public static class RoutineQuiz
{
    public static Quiz Instance { get; } = new([
        new("Question 1", ["Option A", "Option B", "Option C"]),
        new("Question 2", ["Option A", "Option B", "Option C"]),
    ]);
}
