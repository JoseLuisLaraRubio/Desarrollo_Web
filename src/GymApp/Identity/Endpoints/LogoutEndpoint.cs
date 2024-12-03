namespace GymApp.Identity.Endpoints;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public static class LogoutEndpoint
{
    public static Task Handle<TUser>(
        [FromServices] SignInManager<TUser> signInManager)
        where TUser : AppIdentityUser
    {
        return signInManager.SignOutAsync();
    }
}
