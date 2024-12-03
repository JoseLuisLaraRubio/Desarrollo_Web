namespace GymApp.ApiService.Features.Quizzes.Data;

public sealed record Quiz(
    IReadOnlyCollection<QuizQuestion> Questions);
