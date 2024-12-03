namespace GymApp.Database;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class AppDbContextServiceCollectionExtensions
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<AppDbContext>(
            options => AppDbContextConfiguration.Configure(options, configuration));
    }
}
