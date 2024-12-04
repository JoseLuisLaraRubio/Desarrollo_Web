namespace GymApp.ApiService.Features.Routines.Data;

using FluentValidation;

using GymApp.Database.Entities.Routines;

using RaptorUtils.Collections.Extensions;

#pragma warning disable SA1402

public record RoutineUpdate(
    string Name,
    IReadOnlyCollection<WorkoutUpdate> Workouts)
{
    public void Apply(Routine routine)
    {
        routine.Name = this.Name;

        Dictionary<Guid, WorkoutUpdate> workoutUpdateMap = this.GetWorkoutUpdateMap();

        routine.Workouts.RemoveAll(w => !workoutUpdateMap.ContainsKey(w.Id));

        foreach (var workout in routine.Workouts)
        {
            WorkoutUpdate workoutUpdate = workoutUpdateMap[workout.Id];
            workoutUpdate.Apply(workout);
        }

        routine.Workouts.AddRange(this.GetNewWorkouts());
    }

    private Dictionary<Guid, WorkoutUpdate> GetWorkoutUpdateMap()
    {
        return this.Workouts
            .Where(w => w.Id.HasValue)
            .ToDictionary(w => w.Id!.Value);
    }

    private IEnumerable<Workout> GetNewWorkouts()
    {
        return this.Workouts
            .Where(b => !b.Id.HasValue)
            .Select(b => b.ToWorkout());
    }

    public sealed class Validator : AbstractValidator<RoutineUpdate>
    {
        public Validator()
        {
            this.RuleFor(r => r.Name).NotEmpty();

            this.RuleFor(r => r.Workouts).NotNull()
                .ForEach(w => w.SetValidator(WorkoutUpdate.Validator.Instance));
        }
    }
}

public record WorkoutUpdate(
    Guid? Id,
    IReadOnlyCollection<ExerciseBlockUpdate> Blocks)
{
    public void Apply(Workout workout)
    {
        if (!this.Id.HasValue)
        {
            throw new InvalidOperationException("A update with no id can not be applied.");
        }

        Dictionary<Guid, ExerciseBlockUpdate> blockUpdateMap = this.GetBlockUpdateMap();

        workout.Blocks.RemoveAll(b => !blockUpdateMap.ContainsKey(b.Id));

        foreach (var block in workout.Blocks)
        {
            ExerciseBlockUpdate blockUpdate = blockUpdateMap[block.Id];
            blockUpdate.Apply(block);
        }

        workout.Blocks.AddRange(this.GetNewBlocks());
    }

    public Workout ToWorkout()
    {
        return this.Id.HasValue
            ? throw new InvalidOperationException("A workout should only be created if the id is null.")
            : new()
            {
                Blocks = this.Blocks.Select(b => b.ToBlock()).ToList(),
            };
    }

    private Dictionary<Guid, ExerciseBlockUpdate> GetBlockUpdateMap()
    {
        return this.Blocks
            .Where(w => w.Id.HasValue)
            .ToDictionary(w => w.Id!.Value);
    }

    private IEnumerable<ExerciseBlock> GetNewBlocks()
    {
        return this.Blocks
            .Where(b => !b.Id.HasValue)
            .Select(b => b.ToBlock());
    }

    public sealed class Validator : AbstractValidator<WorkoutUpdate>
    {
        public Validator()
        {
            this.RuleFor(w => w.Id).NotEqual(Guid.Empty);

            this.RuleFor(w => w.Blocks).NotNull()
                .ForEach(w => w.SetValidator(ExerciseBlockUpdate.Validator.Instance));
        }

        public static Validator Instance { get; } = new();
    }
}

public record ExerciseBlockUpdate(
    Guid? Id,
    Guid ExerciseId,
    int Sets,
    int Repetitions)
{
    public void Apply(ExerciseBlock block)
    {
        if (!this.Id.HasValue)
        {
            throw new InvalidOperationException("A update with no id can not be applied.");
        }

        block.Exercise = null!;
        block.ExerciseId = this.ExerciseId;
        block.Sets = this.Sets;
        block.Repetitions = this.Repetitions;
    }

    public ExerciseBlock ToBlock()
    {
        return this.Id.HasValue
            ? throw new InvalidOperationException("A block should only be created if the id is null.")
            : new()
            {
                Exercise = null!,
                ExerciseId = this.ExerciseId,
                Sets = this.Sets,
                Repetitions = this.Repetitions,
            };
    }

    public sealed class Validator : AbstractValidator<ExerciseBlockUpdate>
    {
        public Validator()
        {
            this.RuleFor(b => b.Id).NotEqual(Guid.Empty);

            this.RuleFor(b => b.ExerciseId).NotEqual(Guid.Empty);

            this.RuleFor(b => b.Sets).GreaterThan(0);

            this.RuleFor(b => b.Repetitions).GreaterThan(0);
        }

        public static Validator Instance { get; } = new();
    }
}
