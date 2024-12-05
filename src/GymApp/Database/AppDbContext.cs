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
        optionsBuilder
            .UseValidationCheckConstraints()
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

        ConfigurePropertiesAsString<Muscle>();
        ConfigurePropertiesAsString<Equipment>();
        ConfigurePropertiesAsString<Sex>();
        ConfigurePropertiesAsString<BodyType>();

        void ConfigurePropertiesAsString<T>()
            => configurationBuilder.Properties<T>().HaveConversion<string>().HaveColumnType("VARCHAR(255)");
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        var appNamespace = new Guid("f3cf2703-30de-4ca2-a10f-1f2f63a2972c");
        var roleGenerator = new IdentityRoleGenerator(appNamespace);

        IEnumerable<IdentityRole> roles = roleGenerator.Generate<UserRole>();

        builder.Entity<IdentityRole>().HasData(roles);
    }

    private static void SeedExercises(ModelBuilder builder)
    {
        builder.Entity<Exercise>().HasData(
            new()
            {
                Id = new Guid("D66F0C4F-FF37-4B45-9C6C-B3E5413C9D33"),
                Name = "Remo con barra",
                PrimaryMuscle = Muscle.MidBack,
                SecondaryMuscles = [Muscle.Lats, Muscle.Biceps],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("61F51B7E-5F1A-463C-B8A4-29A02BB26A47"),
                Name = "Remo con mancuernas",
                PrimaryMuscle = Muscle.MidBack,
                SecondaryMuscles = [Muscle.Lats, Muscle.Biceps],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 5,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("42F3E09B-8D9B-4657-A25C-80DB350BCDC2"),
                Name = "Dominadas neutras",
                PrimaryMuscle = Muscle.Lats,
                SecondaryMuscles = [Muscle.MidBack, Muscle.Biceps],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 8,
                EffectivenessLevel = 10,
            },
            new()
            {
                Id = new Guid("F42F75F3-6E48-44A2-BB32-1AB27801540C"),
                Name = "Remo en máquina o polea baja",
                PrimaryMuscle = Muscle.MidBack,
                SecondaryMuscles = [Muscle.Lats, Muscle.Biceps],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 2,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("CE44A0A4-B76C-4A55-B9F1-A656D302D8A6"),
                Name = "Face pulls con banda elástica",
                PrimaryMuscle = Muscle.MidBack,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Lats],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 3,
                EffectivenessLevel = 3,
            },
            new()
            {
                Id = new Guid("DD452D7C-36B2-457B-9A44-F8A9BE4F8C23"),
                Name = "Dominadas pronadas",
                PrimaryMuscle = Muscle.Lats,
                SecondaryMuscles = [Muscle.MidBack, Muscle.Biceps],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 8,
                EffectivenessLevel = 10,
            },
            new()
            {
                Id = new Guid("B48D6D12-93E4-473D-8D9D-6F378A58DA47"),
                Name = "Jalón al pecho en polea",
                PrimaryMuscle = Muscle.Lats,
                SecondaryMuscles = [Muscle.MidBack, Muscle.Biceps],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 5,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("56201C2C-A6F4-4678-92E9-2F8E4CC1A024"),
                Name = "Remo con mancuerna a una mano",
                PrimaryMuscle = Muscle.Lats,
                SecondaryMuscles = [Muscle.MidBack, Muscle.Biceps],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 5,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("B726D1AB-BFA2-44F2-B6B9-A3E7B400A6FC"),
                Name = "Pullover con mancuerna",
                PrimaryMuscle = Muscle.Lats,
                SecondaryMuscles = [Muscle.MidBack, Muscle.Shoulders],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("8FE4D6D3-BD02-4025-B560-B4E23C1D7E6E"),
                Name = "Pull-ups asistidos con banda",
                PrimaryMuscle = Muscle.Lats,
                SecondaryMuscles = [Muscle.MidBack, Muscle.Biceps],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 5,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("A946E8E0-33B4-48B3-97B6-3B6DB9B20297"),
                Name = "Press de banca con barra",
                PrimaryMuscle = Muscle.Chest,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Triceps],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("79CB5F5A-BF5F-45C3-94FC-62B4471B1AB5"),
                Name = "Press de banca con mancuernas",
                PrimaryMuscle = Muscle.Chest,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Triceps],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 5,
                EffectivenessLevel = 10,
            },
            new()
            {
                Id = new Guid("9B08D7B7-8C80-4F1C-8B12-B283C917BCF3"),
                Name = "Flexiones",
                PrimaryMuscle = Muscle.Chest,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Triceps],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("3F78DA3F-F3D2-4C38-86B5-1B041AE149D3"),
                Name = "Aperturas con mancuernas",
                PrimaryMuscle = Muscle.Chest,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Triceps],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 4,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("BFC6A453-6939-45B2-9245-ED54F93C14A7"),
                Name = "Fondos en paralelas",
                PrimaryMuscle = Muscle.Chest,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Triceps],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("1D34383A-2B5A-42B6-91B7-8A2B9F55E48F"),
                Name = "Sentadilla con barra",
                PrimaryMuscle = Muscle.Quads,
                SecondaryMuscles = [Muscle.Glutes, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 8,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("C919E6A1-1D5A-4F84-BAF9-0ECAB05A7DA5"),
                Name = "Sentadilla goblet con mancuerna",
                PrimaryMuscle = Muscle.Quads,
                SecondaryMuscles = [Muscle.Glutes, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 2,
                EffectivenessLevel = 6,
            },
            new()
            {
                Id = new Guid("4D4433C2-F71A-4334-B517-80CE6A0BCE65"),
                Name = "Sentadillas con peso corporal",
                PrimaryMuscle = Muscle.Quads,
                SecondaryMuscles = [Muscle.Glutes, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 1,
                EffectivenessLevel = 3,
            },
            new()
            {
                Id = new Guid("9F4A34B7-AE60-4F37-8AA4-D71F4C7171F0"),
                Name = "Zancadas caminando con mancuernas",
                PrimaryMuscle = Muscle.Quads,
                SecondaryMuscles = [Muscle.Glutes, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("F9EEC319-35A3-4633-AF49-83B2F5C4BB11"),
                Name = "Extensiones de piernas en máquina",
                PrimaryMuscle = Muscle.Quads,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 1,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("C88FBA70-6A31-4B79-B7B3-1E8A365B635C"),
                Name = "Puentes de glúteos con mancuerna",
                PrimaryMuscle = Muscle.Glutes,
                SecondaryMuscles = [Muscle.Quads, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("B084C8D4-08A2-469C-B2A2-F54375C15A9B"),
                Name = "Hip thrust",
                PrimaryMuscle = Muscle.Glutes,
                SecondaryMuscles = [Muscle.Quads, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("AC41BCE5-5229-4699-9140-CE57DA467A95"),
                Name = "Sentadilla sumo",
                PrimaryMuscle = Muscle.Glutes,
                SecondaryMuscles = [Muscle.Quads, Muscle.Hamstrings],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("5A900433-0C8D-4131-B83D-8E1A92D03296"),
                Name = "Abducción de cadera con banda elástica",
                PrimaryMuscle = Muscle.Glutes,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 6,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("A6F7F8C1-12C4-4A36-868F-8D2C4979285F"),
                Name = "Curl de pierna acostado",
                PrimaryMuscle = Muscle.Hamstrings,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("DA2DC2AC-9A87-4644-990D-462BEE68F1EE"),
                Name = "Curl femoral con mancuerna",
                PrimaryMuscle = Muscle.Hamstrings,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("E62DA2AB-ED96-4C4A-B509-226A88592578"),
                Name = "Peso muerto con piernas rígidas",
                PrimaryMuscle = Muscle.Hamstrings,
                SecondaryMuscles = [Muscle.Glutes],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 8,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("D7262A35-9C26-4208-9879-6A5F5F647D0C"),
                Name = "Hip thrust con una pierna",
                PrimaryMuscle = Muscle.Hamstrings,
                SecondaryMuscles = [Muscle.Glutes],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("CC1A7288-1E5A-46AE-BD9F-1E1E0C3C0E9F"),
                Name = "Desplantes",
                PrimaryMuscle = Muscle.Hamstrings,
                SecondaryMuscles = [Muscle.Glutes, Muscle.Quads],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("5D7F689E-58C0-4F22-B6E7-04A3C80F4705"),
                Name = "Elevaciones de talones de pie",
                PrimaryMuscle = Muscle.Calves,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 5,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("9C73F2F3-B188-4D7F-B38C-99C28D040D34"),
                Name = "Elevaciones de talones sentado",
                PrimaryMuscle = Muscle.Calves,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 5,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("D4D8A7C9-5CC8-46A0-B5FE-53B50B3F9FE9"),
                Name = "Elevaciones de talones con mancuerna",
                PrimaryMuscle = Muscle.Calves,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("9F9F94F0-FB6F-4C2A-88E7-88D1332A8CC0"),
                Name = "Saltar la cuerda",
                PrimaryMuscle = Muscle.Calves,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("16357F68-A69B-40A0-97DB-A7B4F4D899D4"),
                Name = "Sprint en colina",
                PrimaryMuscle = Muscle.Calves,
                SecondaryMuscles = [Muscle.Quads, Muscle.Glutes],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("AB9F1ACD-420C-4D91-8CC2-B75C5B1A6344"),
                Name = "Curl de bíceps con barra",
                PrimaryMuscle = Muscle.Biceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("A30B9745-69A1-4D67-88D5-91AEAFBC0A68"),
                Name = "Curl de bíceps con mancuerna",
                PrimaryMuscle = Muscle.Biceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("E9B54A82-7A02-47F6-B038-4DAD1EAD4BE9"),
                Name = "Curl de bíceps en predicador",
                PrimaryMuscle = Muscle.Biceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("C82D8100-B6B0-45E1-8D8F-7D8B65D7A70E"),
                Name = "Curl de bíceps con banda elástica",
                PrimaryMuscle = Muscle.Biceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 6,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("54EED5B9-2958-4F0C-AF04-0D6F67C4B8B4"),
                Name = "Chin-ups",
                PrimaryMuscle = Muscle.Biceps,
                SecondaryMuscles = [Muscle.Lats],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 8,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("E76A0FF3-02A9-4F90-9FC7-1B3A65F5A0A4"),
                Name = "Fondos en paralelas",
                PrimaryMuscle = Muscle.Triceps,
                SecondaryMuscles = [Muscle.Shoulders, Muscle.Chest],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 7,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("8D9BB7D9-BD54-4B2E-9D2F-52F96CCB2833"),
                Name = "Press de triceps en cuerda",
                PrimaryMuscle = Muscle.Triceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("B28D349D-4BC9-488F-AC9F-7EC0A914C660"),
                Name = "Extensiones de triceps con mancuerna",
                PrimaryMuscle = Muscle.Triceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("5BE8D8E7-84F0-4506-B20C-9A3F4CC3B702"),
                Name = "Patada de triceps",
                PrimaryMuscle = Muscle.Triceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("91D9F126-1796-402C-AF8F-8D58A345D620"),
                Name = "Press de triceps en banco",
                PrimaryMuscle = Muscle.Triceps,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("5D55881C-98C2-47D1-80E5-731F3C265611"),
                Name = "Press militar con barra",
                PrimaryMuscle = Muscle.Shoulders,
                SecondaryMuscles = [Muscle.Triceps, Muscle.Chest],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 8,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("46D57D89-9F06-4C88-84F9-B12DEB4A37B9"),
                Name = "Press militar con mancuernas",
                PrimaryMuscle = Muscle.Shoulders,
                SecondaryMuscles = [Muscle.Triceps, Muscle.Chest],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 7,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("C4B6C5D7-159C-4B77-A2A4-9A2D24B4E44A"),
                Name = "Elevaciones laterales",
                PrimaryMuscle = Muscle.Shoulders,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("229503E7-1155-40E0-94D4-04C0A7462D97"),
                Name = "Elevaciones frontales",
                PrimaryMuscle = Muscle.Shoulders,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("53C97DBA-973F-4CBF-BC87-B4E89F2A64D6"),
                Name = "Arnold press",
                PrimaryMuscle = Muscle.Shoulders,
                SecondaryMuscles = [Muscle.Triceps],
                EquipmentRequirement = Equipment.Dumbbells,
                DifficultyLevel = 7,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("F4A3C89B-CF6E-4428-8A3B-0C1160190BC3"),
                Name = "Plancha (Plank)",
                PrimaryMuscle = Muscle.Abs,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 9,
            },
            new()
            {
                Id = new Guid("024F05D5-43D0-4F4E-A8E7-708D5E72707E"),
                Name = "Crunches",
                PrimaryMuscle = Muscle.Abs,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 5,
                EffectivenessLevel = 7,
            },
            new()
            {
                Id = new Guid("BD3FB9F6-C9D5-4D2D-98D7-2F951D9C96D5"),
                Name = "Elevación de piernas en banco",
                PrimaryMuscle = Muscle.Abs,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("3DA5EC52-1E31-4D44-B04C-A1E6078EE676"),
                Name = "Crunch con cable",
                PrimaryMuscle = Muscle.Abs,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.Gym,
                DifficultyLevel = 7,
                EffectivenessLevel = 8,
            },
            new()
            {
                Id = new Guid("F8018C87-5FE0-4852-9DE9-FD5719D591D3"),
                Name = "Russian twists",
                PrimaryMuscle = Muscle.Abs,
                SecondaryMuscles = [],
                EquipmentRequirement = Equipment.None,
                DifficultyLevel = 6,
                EffectivenessLevel = 8,
            });
    }
}
