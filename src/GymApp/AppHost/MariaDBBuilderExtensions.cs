namespace GymApp.AppHost;

using EFMigrationService.Integration;

using Microsoft.Extensions.Configuration;

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

    private static IResourceBuilder<MySqlServerResource> AddMariaDB(
        this IDistributedApplicationBuilder builder,
        string name,
        IResourceBuilder<ParameterResource>? password = null,
        int? port = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(name);

        var passwordParameter = password?.Resource
            ?? ParameterResourceBuilderExtensions.CreateDefaultPasswordParameter(builder, $"{name}-password");

        var resource = new MySqlServerResource(name, passwordParameter);
        return builder
            .AddResource(resource)
            .WithEndpoint(port: port, targetPort: 3306, name: "tcp")
            .WithImage("library/mariadb", "11.5.2")
            .WithImageRegistry("docker.io")
            .WithEnvironment(
                context => context.EnvironmentVariables["MYSQL_ROOT_PASSWORD"] = resource.PasswordParameter);
    }
}
