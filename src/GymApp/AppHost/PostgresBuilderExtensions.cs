namespace GymApp.AppHost;

using EFMigrationService.Integration;

using Microsoft.Extensions.Configuration;

internal static class PostgresBuilderExtensions
{
    public static IResourceBuilder<PostgresServerResource> AddConfiguredPostgres(
        this IDistributedApplicationBuilder builder,
        IConfiguration configuration)
    {
        var postgres = builder.AddPostgres("Postgres")
            .WithPgAdmin()
            .WithPgWeb();

        if (configuration.IsDatabasePersistenceEnabled())
        {
            return postgres.WithDataVolume();
        }

        return postgres;
    }
}
