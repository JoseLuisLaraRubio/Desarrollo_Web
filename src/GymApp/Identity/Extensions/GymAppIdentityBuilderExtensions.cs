namespace GymApp.Identity.Extensions;

using GymApp.Identity.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class GymAppIdentityBuilderExtensions
{
    public static IdentityBuilder AddGymAppApiEndpoints<TUser>(this IdentityBuilder builder)
        where TUser : AppIdentityUser, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.TryAddTransient<UserRegisterService<TUser>>();
        builder.Services.TryAddTransient<AdminCodeValidator>();

        return builder;
    }
}
