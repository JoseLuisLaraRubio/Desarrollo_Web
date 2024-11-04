using EFMigrationService.Integration;

using GymApp.AppHost;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RaptorUtils.Aspire.Hosting.NodeJs;
using RaptorUtils.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var mariaDB = builder.AddConfiguredMariaDB(builder.Configuration);

var gymAppDb = mariaDB.AddDatabase("GymAppDb");

if (!builder.Configuration.IsDatabaseMigrationMode())
{
    var apiService = builder.AddProject<Projects.GymApp_ApiService>("ApiService")
        .WithReference(gymAppDb);

    builder.AddNpmApp("WebApp", "../WebApp")
        .WithEnvironment("SERVER_URL", apiService.GetEndpoint("http"))
        .WithRandomPort();
}
else
{
    using var serviceProvider = builder.Services.BuildServiceProvider();

    var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<MigrationService>();

    string efToolVersion = builder.Configuration.GetRequired("EFToolVersion");

    var migrationService = new MigrationService(logger, "../../EFMigrationService", efToolVersion);

    await migrationService.Initialize();

    string migrationTargetProject = Path.GetFullPath("../Database");

    var migrationServer = builder.AddProject<Projects.EFMigrationService_Server>("MigrationServer")
        .WithReference(gymAppDb)
        .WithSingleMigrationProject(migrationTargetProject);

    builder.AddMigrationClient(migrationServer.GetEndpoint("http"), migrationService.ClientPath);
}

await builder.Build().RunAsync();
