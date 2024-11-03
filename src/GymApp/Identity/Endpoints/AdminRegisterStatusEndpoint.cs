namespace GymApp.Identity.Endpoints;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class AdminRegisterStatusEndpoint
{
    public static async Task<Ok<bool>> Handle<TUser>(
        [FromServices] UserManager<TUser> userManager)
        where TUser : AppIdentityUser
    {
        bool anyUser = await userManager.Users.AnyAsync();
        return TypedResults.Ok(anyUser);
    }
}
