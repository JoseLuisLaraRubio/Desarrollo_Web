namespace GymApp.AppHost;

using EFMigrationService.Integration;

using Microsoft.Extensions.Configuration;

using RaptorUtils.Aspire.Hosting.MySql;

internal static class MariaDBBuilderExtensions
{
    public static IResourceBuilder<MySqlServerResource> AddConfiguredMariaDB(
        this IDistributedApplicationBuilder builder,
        IConfiguration configuration)
    {
        return AddConfiguredMariaDB().WithLifetime(ContainerLifetime.Persistent);

        IResourceBuilder<MySqlServerResource> AddConfiguredMariaDB()
        {
            var mySql = builder.AddMariaDB("MariaDB")
                .WithPhpMyAdmin();

            return configuration.IsDatabasePersistenceEnabled()
                ? mySql.WithDataVolume()
                : mySql;
        }
    }
}
