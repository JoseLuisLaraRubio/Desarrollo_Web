namespace GymApp.ApiService.Features.Progress.Data;

using FluentValidation;

#pragma warning disable SA1402

public record WorkoutProgressData(
    DateTimeOffset Date,
    ICollection<ExerciseBlockResultData> Results)
{
    public WorkoutProgressDataWithId WithId(Guid workoutId)
    {
        return new WorkoutProgressDataWithId(workoutId, this.Date, this.Results);
    }

    public class Validator : AbstractValidator<WorkoutProgressData>
    {
        public Validator()
        {
            this.RuleFor(r => r.Date).Must(d => d.Offset == TimeSpan.Zero)
                .WithMessage("The DateTimeOffset must be in UTC.");

            this.RuleFor(r => r.Results).NotNull()
                .ForEach(r => r.SetValidator(ExerciseBlockResultData.Validator.Instance));
        }
    }
}

public record WorkoutProgressDataWithId(
    Guid WorkoutId,
    DateTimeOffset Date,
    ICollection<ExerciseBlockResultData> Results)
    : WorkoutProgressData(Date, Results);

public record ExerciseBlockResultData(
    Guid ExerciseId,
    ICollection<SetResultData> Sets)
{
    public sealed class Validator : AbstractValidator<ExerciseBlockResultData>
    {
        public Validator()
        {
            this.RuleFor(b => b.ExerciseId).NotEmpty();

            this.RuleFor(b => b.Sets).NotNull()
                .ForEach(s => s.SetValidator(SetResultData.Validator.Instance));
        }

        public static Validator Instance { get; } = new Validator();
    }
}

public record SetResultData(
    float Weight,
    int Repetitions)
{
    public sealed class Validator : AbstractValidator<SetResultData>
    {
        public Validator()
        {
            this.RuleFor(s => s.Weight).GreaterThan(0);

            this.RuleFor(s => s.Repetitions).GreaterThanOrEqualTo(1);
        }

        public static Validator Instance { get; } = new Validator();
    }
}
