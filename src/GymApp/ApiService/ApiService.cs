namespace GymApp.ApiService;

using GymApp.Database;
using GymApp.Database.Entities;
using GymApp.Identity.Extensions;
using GymApp.ServiceDefaults.WebAppSettings;

using Microsoft.AspNetCore.Builder;

public class ApiService : GymAppWebAppDefinition
{
    protected override async Task ConfigureServices(WebApplicationBuilder builder)
    {
        await base.ConfigureServices(builder);

        builder.Services.AddProblemDetails();

        builder.Services.AddAppDbContext(builder.Configuration);

        builder.Services.AddAppIdentity<AppDbContext, AppUser>(builder.Configuration);

        builder.Services.AddAppAuth();
    }

    protected override async Task Configure(WebApplication app)
    {
        await base.Configure(app);

        app.UseExceptionHandler();

        var apiGroup = app.MapGroup("/api");

        apiGroup.MapGymAppIdentityApi<AppUser>();

        await app.InitializeDb();
    }
}
