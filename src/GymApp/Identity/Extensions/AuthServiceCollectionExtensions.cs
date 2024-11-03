namespace GymApp.Identity.Extensions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class AuthServiceCollectionExtensions
{
    public static void AddAppAuth(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

        services.AddAuthorizationBuilder()
            .AddPolicy(AppPolicy.AdminRole.Name, AppPolicy.AdminRole.Create());

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "GymAppIdentity";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.SlidingExpiration = true;

            options.Events.OnRedirectToLogin
                = (context) => RedirectResponse(context, StatusCodes.Status401Unauthorized);

            options.Events.OnRedirectToAccessDenied
                = (context) => RedirectResponse(context, StatusCodes.Status403Forbidden);

            static Task RedirectResponse(RedirectContext<CookieAuthenticationOptions> context, int status)
            {
                context.Response.StatusCode = status;
                return Task.CompletedTask;
            }
        });
    }
}
