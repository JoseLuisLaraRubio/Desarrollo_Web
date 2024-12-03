namespace GymApp.Identity.Extensions;

using GymApp.Identity;
using GymApp.Identity.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RaptorUtils.AspNet.Identity;

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection AddAppIdentity<TDbContext, TUser, TUserRegistrationHandler>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : IdentityDbContext<TUser>
        where TUser : AppIdentityUser, new()
        where TUserRegistrationHandler : class, IUserRegistrationHandler<TUser>
    {
        int passwordMinLength = configuration.GetRequiredSection("PasswordMinLength").Get<int>();

        services.AddIdentityCore<TUser>(options =>
        {
            options.Password.RequiredLength = passwordMinLength;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<TDbContext>()
        .AddUserManager<UserManager<TUser>>()
        .AddSignInManager<SignInManager<TUser>>()
        .AddGymAppApiEndpoints<TUser>();

        services.AddScoped<UserContext<TUser>>();
        services.AddScoped<TUserRegistrationHandler>();
        services.AddScoped<IUserRegistrationHandler<TUser>, TUserRegistrationHandler>();

        return services;
    }
}
