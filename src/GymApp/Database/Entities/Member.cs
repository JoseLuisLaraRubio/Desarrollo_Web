namespace GymApp.Database.Entities;

using GymApp.Database.Entities.Routines;

public class Member
{
    public required AppUser User { get; init; }

    public ICollection<Routine> Routines { get; init; } = [];
}
