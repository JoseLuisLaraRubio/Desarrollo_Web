namespace GymApp.AppHost;

using EFMigrationService.Integration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RaptorUtils.Extensions.Configuration;

internal static class MigrationServiceExtensions
{
    public static async Task<IDistributedApplicationBuilder> AddConfiguredMigrationService(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<IResourceWithConnectionString> database)
    {
        await using var serviceProvider = builder.Services.BuildServiceProvider();

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<MigrationService>();

        string efToolVersion = builder.Configuration.GetRequired("EFToolVersion");

        var migrationService = new MigrationService(logger, "../../EFMigrationService", efToolVersion);

        await migrationService.Initialize();

        string migrationTargetProject = Path.GetFullPath("../Database");

        var migrationServer = builder.AddProject<Projects.EFMigrationService_Server>("MigrationServer")
            .WithReference(database)
            .WithSingleMigrationProject(migrationTargetProject);

        builder.AddMigrationClient(migrationServer.GetEndpoint("http"), migrationService.ClientPath);

        return builder;
    }
}
