namespace GymApp.ApiService.Features.Progress.Data;

using FluentValidation;

#pragma warning disable SA1402

public record RoutineProgressData(
    DateTimeOffset Date,
    ICollection<RoutineBlockResultData> Results)
{
    public RoutineProgressDataWithId WithId(Guid routineId)
    {
        return new RoutineProgressDataWithId(routineId, this.Date, this.Results);
    }

    public class Validator : AbstractValidator<RoutineProgressData>
    {
        public Validator()
        {
            this.RuleFor(r => r.Date).Must(d => d.Offset == TimeSpan.Zero)
                .WithMessage("The DateTimeOffset must be in UTC.");

            this.RuleFor(r => r.Results).NotNull()
                .ForEach(r => r.SetValidator(RoutineBlockResultData.Validator.Instance));
        }
    }
}

public record RoutineProgressDataWithId(
    Guid RoutineId,
    DateTimeOffset Date,
    ICollection<RoutineBlockResultData> Results)
    : RoutineProgressData(Date, Results);

public record RoutineBlockResultData(
    Guid ExerciseId,
    ICollection<SetResultData> Sets)
{
    public sealed class Validator : AbstractValidator<RoutineBlockResultData>
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
