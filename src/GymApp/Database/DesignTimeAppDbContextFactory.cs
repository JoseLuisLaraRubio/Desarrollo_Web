namespace GymApp.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

internal class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        AppDbContextConfiguration.Configure(optionsBuilder, configuration);

        return new AppDbContext(optionsBuilder.Options);
    }
}
