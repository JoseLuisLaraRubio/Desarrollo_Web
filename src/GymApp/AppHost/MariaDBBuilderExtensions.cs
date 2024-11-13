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
        var mySql = builder.AddMariaDB("MariaDB")
            .WithPhpMyAdmin();

        if (configuration.IsDatabasePersistenceEnabled())
        {
            return mySql.WithDataVolume();
        }

        return mySql;
    }
}
