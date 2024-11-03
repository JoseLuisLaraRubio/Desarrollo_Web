namespace GymApp.Identity.Endpoints;

using GymApp.Identity;
using GymApp.Identity.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using RaptorUtils.AspNet.Identity;

public static class UserInfoEndpoint
{
    public static async Task<Results<Ok<UserInfoResponse>, NotFound>> HandleGet<TUser>(
        [FromServices] UserContext<TUser> userContext)
        where TUser : AppIdentityUser
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.NotFound();
        }

        var info = new UserInfoResponse()
        {
            UserName = user.UserName,
        };

        return TypedResults.Ok(info);
    }
}
