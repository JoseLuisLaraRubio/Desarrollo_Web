namespace GymApp.ApiService.Features.Progress.Endpoints;

public static class ProgressApiExtensions
{
    public static IEndpointConventionBuilder MapProgressApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup(string.Empty);

        routeGroup.MapGet("{workoutId}/progress", ProgressEndpoints.HandleGet);

        routeGroup.MapPost("{workoutId}/progress", ProgressEndpoints.HandlePost);

        return routeGroup;
    }
}
