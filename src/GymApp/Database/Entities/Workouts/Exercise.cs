namespace GymApp.Database.Entities.Workouts;

using System.ComponentModel.DataAnnotations;

public class Exercise
{
    public Guid Id { get; init; }

    [MaxLength(50)]
    public required string Name { get; init; }

    public required Muscle PrimaryMuscle { get; init; }

    public ICollection<Muscle> SecondaryMuscles { get; set; } = [];

    public required Equipment EquipmentRequirement { get; init; }

    [Range(1, 10)]
    public required int DifficultyLevel { get; set; }

    [Range(1, 10)]
    public required int EffectivenessLevel { get; set; }
}
