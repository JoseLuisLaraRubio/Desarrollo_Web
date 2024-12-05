namespace GymApp.ApiService.Features.PersonalInfo.Endpoints;

public static class PersonalInfoApiExtensions
{
    public static IEndpointConventionBuilder MapPersonalInfoApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/personal-info")
            .RequireAuthorization();

        routeGroup.MapGet(string.Empty, PersonalInfoEndpoints.HandleGet);

        routeGroup.MapPut(string.Empty, PersonalInfoEndpoints.HandlePut);

        routeGroup.MapGet("/picture", PersonalInfoEndpoints.HandlePictureGet);

        routeGroup.MapPut("/picture", PersonalInfoEndpoints.HandlePicturePut)
            .DisableAntiforgery();

        return routeGroup.WithTags("Personal info");
    }
}
