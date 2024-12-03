namespace GymApp.ApiService.Features.Progress.Endpoints;

public static class ProgressApiExtensions
{
    public static IEndpointConventionBuilder MapProgressApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/workouts");

        routeGroup.MapPost("{workoutId}/progress", ProgressEndpoints.HandlePost);

        return routeGroup;
    }
}
