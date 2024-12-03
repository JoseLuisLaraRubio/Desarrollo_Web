namespace GymApp.ApiService.Features.Quizzes.Data;

public sealed record QuizQuestion(
    string Question,
    IReadOnlyCollection<string> Options);
