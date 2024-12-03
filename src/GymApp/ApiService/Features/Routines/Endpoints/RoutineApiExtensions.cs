namespace GymApp.ApiService.Features.Routines.Endpoints;

using GymApp.ApiService.Features.Progress.Endpoints;

public static class RoutineApiExtensions
{
    public static IEndpointConventionBuilder MapRoutineApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/routines")
            .RequireAuthorization();

        routeGroup.MapGet("/current", WorkoutEndpoints.HandleGet);

        routeGroup.MapProgressApi();

        return routeGroup.WithTags("Routine");
    }
}
