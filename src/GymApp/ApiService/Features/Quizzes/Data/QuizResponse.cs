namespace GymApp.ApiService.Features.Quizzes.Data;

using FluentValidation;

public record QuizResponse(IReadOnlyCollection<int> AnswersIndices)
{
    public class Validator : AbstractValidator<QuizResponse>
    {
        public Validator()
        {
            this.RuleFor(r => r.AnswersIndices).NotNull()
                .ForEach(i => i.GreaterThanOrEqualTo(0));
        }
    }
}
