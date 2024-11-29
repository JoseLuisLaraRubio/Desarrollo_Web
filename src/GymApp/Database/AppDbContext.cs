namespace GymApp.Database;

using GymApp.Database.Entities;
using GymApp.Database.Entities.Routines;
using GymApp.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RaptorUtils.AspNet.Identity;
using RaptorUtils.EntityFramework.Metadata.Builders.Extensions;

public class AppDbContext(
    DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    public DbSet<Member> Members { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseValidationCheckConstraints()
            .UseEnumCheckConstraints();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Member>(b =>
        {
            b.HasKey($"{nameof(Member.User)}Id");
            b.HasOne(m => m.User).WithOne().IsRequired();
        });

        builder.Entity<Exercise>()
            .Property(e => e.SecondaryMuscles)
            .HasJsonConversion();

        SeedRoles(builder);
        SeedExercises(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<Muscle>().HaveConversion<string>().HaveColumnType("VARCHAR(255)");
        configurationBuilder.Properties<Equipment>().HaveConversion<string>().HaveColumnType("VARCHAR(255)");
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        var appNamespace = new Guid("f3cf2703-30de-4ca2-a10f-1f2f63a2972c");
        var roleGenerator = new IdentityRoleGenerator(appNamespace);

        IEnumerable<IdentityRole> roles = roleGenerator.Generate<UserRole>();

        builder.Entity<IdentityRole>().HasData(roles);
    }

    // TODO: Remove?
    private static void SeedExercises(ModelBuilder builder)
    {
        builder.Entity<Exercise>().HasData(
            new()
            {
                Id = new Guid("05347E42-5B9A-4E12-A2CF-42364BE56F6E"),
                Name = "Bench Press",
                PrimaryMuscle = Muscle.Chest,
                SecondaryMuscles = [Muscle.Shoulders],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 5,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("CEE90C16-9AEC-4795-8D8A-783F13FEC75B"),
                Name = "Squats",
                PrimaryMuscle = Muscle.Legs,
                SecondaryMuscles = [Muscle.Glutes, Muscle.Chest],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 10,
            },
            new()
            {
                Id = new Guid("07C85E4E-C027-46AB-9671-6F606FE92E00"),
                Name = "Deadlift",
                PrimaryMuscle = Muscle.Back,
                SecondaryMuscles = [Muscle.Glutes],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 10,
            });
    }
}
