namespace GymApp.ApiService.Features.PersonalInfo.Endpoints;

using GymApp.ApiService.Features.Members.Services;
using GymApp.Database.Entities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RaptorUtils.AspNet.Identity;

public static class PersonalInfoEndpoints
{
    public static async Task<Results<Ok<PersonalInfo>, NoContent, UnauthorizedHttpResult>> HandleGet(
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] MemberManager memberManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        PersonalInfo? info = await memberManager.Query(user)
            .AsNoTracking()
            .Select(m => m.Info)
            .FirstAsync();

        return info is null
            ? TypedResults.NoContent()
            : TypedResults.Ok(info);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult>> HandlePut(
        [FromBody] PersonalInfo personalInfo,
        [FromServices] UserContext<AppUser> userContext,
        [FromServices] MemberManager memberManager)
    {
        if (await userContext.TryGetLoggedInUser() is not { } user)
        {
            return TypedResults.Unauthorized();
        }

        Member member = await memberManager.Query(user).FirstAsync();
        member.Info = personalInfo;

        await memberManager.SaveChanges();

        return TypedResults.Ok();
    }
}
