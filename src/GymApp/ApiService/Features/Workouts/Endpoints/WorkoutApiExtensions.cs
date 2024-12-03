namespace GymApp.ApiService.Features.Workouts.Endpoints;

using GymApp.ApiService.Features.Progress.Endpoints;

public static class WorkoutApiExtensions
{
    public static IEndpointConventionBuilder MapWorkoutApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/workouts");

        routeGroup.MapGet("{workoutId}", WorkoutEndpoints.HandleGet);

        routeGroup.MapProgressApi();

        return routeGroup;
    }
}
