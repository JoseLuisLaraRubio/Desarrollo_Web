namespace GymApp.Identity.Endpoints;

using GymApp.Identity.Data;
using GymApp.Identity.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public static class RegisterAdminEndpoint
{
    public static async Task<Results<Ok, ValidationProblem>> Handle<TUser>(
        [FromBody] AdminRegisterRequest request,
        [FromServices] UserRegisterService<TUser> userRegisterService)
        where TUser : AppIdentityUser, new()
    {
        IdentityResult result = await userRegisterService.RegisterAdminAsync(request);

        if (!result.Succeeded)
        {
            return IdentityValidationHelper.CreateValidationProblem(result);
        }

        return TypedResults.Ok();
    }
}
