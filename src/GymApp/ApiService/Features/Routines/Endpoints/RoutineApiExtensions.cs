namespace GymApp.ApiService.Features.Routines.Endpoints;

using GymApp.ApiService.Features.Quizzes.Endpoints;

public static class RoutineApiExtensions
{
    public static IEndpointConventionBuilder MapRoutineApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/routines")
            .RequireAuthorization();

        routeGroup.MapGet(string.Empty, RoutineEndpoints.HandleGet);

        routeGroup.MapQuizApi();

        return routeGroup.WithTags("Routine");
    }
}
