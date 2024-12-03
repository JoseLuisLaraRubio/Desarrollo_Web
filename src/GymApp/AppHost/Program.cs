using EFMigrationService.Integration;

using GymApp.AppHost;

using RaptorUtils.Aspire.Hosting.NodeJs;

var builder = DistributedApplication.CreateBuilder(args);

var mariaDB = builder.AddConfiguredMariaDB(builder.Configuration);

var gymAppDb = mariaDB.AddDatabase("GymAppDb");

if (!builder.Configuration.IsDatabaseMigrationMode())
{
    var apiService = builder.AddProject<Projects.GymApp_ApiService>("ApiService")
        .WithHttpHealthCheck("/health")
        .WithReference(gymAppDb)
        .WaitFor(gymAppDb);

    builder.AddNpmApp("WebApp", "../WebApp")
        .WithEnvironment("SERVER_URL", apiService.GetEndpoint("http"))
        .WithRandomPort();
}
else
{
    await builder.AddConfiguredMigrationService(gymAppDb);
}

await builder.Build().RunAsync();
