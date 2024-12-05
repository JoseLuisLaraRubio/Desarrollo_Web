namespace GymApp.ApiService;

using FluentStorage;
using FluentStorage.Blobs;

using FluentValidation;

using GymApp.ApiService.Features.Exercises.Endpoints;
using GymApp.ApiService.Features.Exercises.Services;
using GymApp.ApiService.Features.Members.Services;
using GymApp.ApiService.Features.PersonalInfo.Endpoints;
using GymApp.ApiService.Features.Progress.Services;
using GymApp.ApiService.Features.Quizzes.Endpoints;
using GymApp.ApiService.Features.Routines.Endpoints;
using GymApp.ApiService.Features.Routines.Services;
using GymApp.ApiService.Features.Workouts.Services;
using GymApp.Database;
using GymApp.Database.Entities;
using GymApp.Identity.Extensions;
using GymApp.ServiceDefaults.WebAppSettings;

using Microsoft.AspNetCore.Builder;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

public class ApiService : GymAppWebAppDefinition
{
    protected override async Task ConfigureServices(WebApplicationBuilder builder)
    {
        await base.ConfigureServices(builder);

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        string storagePath = Path.Join(AppContext.BaseDirectory, "uploads");
        IBlobStorage storage = StorageFactory.Blobs.DirectoryFiles(storagePath);

        builder.Services
            .AddProblemDetails()
            .AddAppDbContext(builder.Configuration)
            .AddAppIdentity<AppDbContext, AppUser, MemberManager>(builder.Configuration)
            .AddAppAuth()
            .AddSingleton(storage)
            .AddScoped<ExerciseManager>()
            .AddScoped<RoutineManager>()
            .AddScoped<WorkoutManager>()
            .AddScoped<ProgressManager>()
            .AddValidatorsFromAssemblyContaining<ApiService>(ServiceLifetime.Singleton);
    }

    protected override async Task Configure(WebApplication app)
    {
        await base.Configure(app);

        app.UseExceptionHandler();

        var apiGroup = app.MapGroup("/api").AddFluentValidationAutoValidation();

        apiGroup.MapGymAppIdentityApi<AppUser>();

        apiGroup.MapPersonalInfoApi();

        apiGroup.MapExerciseApi();

        apiGroup.MapRoutineApi();

        apiGroup.MapQuizApi();

        await app.InitializeDb();
    }
}
