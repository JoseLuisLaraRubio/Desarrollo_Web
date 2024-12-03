namespace GymApp.Identity.Extensions;

using GymApp.Identity.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public static class GymAppIdentityApiExtensions
{
    public static IEndpointConventionBuilder MapGymAppIdentityApi<TUser>(
        this IEndpointRouteBuilder endpoints)
        where TUser : AppIdentityUser, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/account");

        routeGroup.MapPost("/register-admin", RegisterAdminEndpoint.Handle<TUser>);

        routeGroup.MapPost("/register", RegisterEndpoint.Handle<TUser>);

        routeGroup.MapPost("/login", LoginEndpoint.Handle<TUser>);

        routeGroup.MapPost("/logout", LogoutEndpoint.Handle<TUser>)
            .RequireAuthorization();

        var accountGroup = routeGroup.MapGroup("/manage");

        accountGroup.MapGet("/info", UserInfoEndpoint.HandleGet<TUser>)
            .RequireAuthorization();

        return routeGroup.WithTags("Account");
    }
}
