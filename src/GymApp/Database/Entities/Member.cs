namespace GymApp.Database.Entities;

using GymApp.Database.Entities.Routines;
using GymApp.Database.Entities.Routines.Progress;

public class Member
{
    public required AppUser User { get; init; }

    public PersonalInfo? Info { get; set; }

    public bool QuizStatus { get; set; }

    public Routine? Routine { get; set; }

    public ICollection<WorkoutProgress> Progress { get; init; } = [];
}
