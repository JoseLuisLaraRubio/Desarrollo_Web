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
            throw new InvalidOperationException("DB_PERSISTENCE is required in production mode");
        }

        app.Logger.LogWarning("Db persistence disabled! Data will not be saved.");

        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // TODO: Replace with aspire 9 WaitFor
        // Wait to avoid a connection retry
        await Task.Delay(TimeSpan.FromSeconds(12));

        bool dbCreated = await AppDbContextInitializer.EnsureCreatedAsync(
            context,
            exception => app.Logger.LogInformation(exception, "Waiting for database..."));

        if (!dbCreated)
        {
            throw new InvalidOperationException("Database already exists.");
        }

        string dbName = context.Database.GetDbConnection().Database;
        app.Logger.LogInformation("Database '{DatabaseName}' created successfully", dbName);
    }
}
