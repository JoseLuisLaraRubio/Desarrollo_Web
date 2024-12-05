namespace GymApp.ApiService.Features.Progress.Data;

#pragma warning disable SA1402

public record WorkoutProgressView(
    DateTimeOffset Date,
    ICollection<ExerciseBlockResultView> Results);

public record ExerciseBlockResultView(
    ExerciseSummaryView Exercise,
    ICollection<SetResultView> Sets);

public record ExerciseSummaryView(
    Guid Id,
    string Name);

public record SetResultView(
    float Weight,
    int Repetitions);
