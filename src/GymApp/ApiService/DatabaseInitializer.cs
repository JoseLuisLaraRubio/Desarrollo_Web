namespace GymApp.ApiService;

using GymApp.Database;

using Microsoft.EntityFrameworkCore;

using RaptorUtils.Extensions.Configuration;

internal static class DatabaseInitializer
{
    public static async Task InitializeDb(this WebApplication app)
    {
        using IServiceScope serviceScope = app.Services.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        await EnsureDbCreated(app, serviceProvider);
    }

    private static async Task EnsureDbCreated(WebApplication app, IServiceProvider serviceProvider)
    {
        if (app.Configuration.IsEnabled("DB_PERSISTENCE"))
        {
            return;
        }

        if (app.Environment.IsProduction())
        {
            throw new InvalidOperationException("DB_PERSISTENCE is required in production mode.");
        }

        app.Logger.LogWarning("Database persistence disabled! Data will not be saved");

        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Database.EnsureCreatedAsync();

        string dbName = dbContext.Database.GetDbConnection().Database;
        app.Logger.LogInformation("Database '{DatabaseName}' created successfully", dbName);
    }
}
