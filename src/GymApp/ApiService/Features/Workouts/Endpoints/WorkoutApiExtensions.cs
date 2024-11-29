namespace GymApp.ApiService.Features.Workouts.Endpoints;

using GymApp.ApiService.Features.Quizzes.Endpoints;

public static class WorkoutApiExtensions
{
    public static IEndpointConventionBuilder MapWorkoutApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/workouts")
            .RequireAuthorization();

        routeGroup.MapGet(string.Empty, WorkoutEndpoints.HandleGet);

        routeGroup.MapQuizApi();

        return routeGroup.WithTags("Workout");
    }
}
