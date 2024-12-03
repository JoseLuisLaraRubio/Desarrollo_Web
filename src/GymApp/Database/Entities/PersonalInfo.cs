namespace GymApp.Database.Entities;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

[Owned]
public class PersonalInfo
{
    [MaxLength(50)]
    public required string FullName { get; set; }

    public required Sex Sex { get; set; }

    public required DateOnly DateOfBirth { get; set; }

    public required int Height { get; set; }

    public required int Weight { get; set; }

    public required BodyType BodyType { get; set; }
}
