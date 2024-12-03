namespace GymApp.ApiService.Features.Exercises.Endpoints;

public static class ExerciseApiExtensions
{
    public static IEndpointConventionBuilder MapExerciseApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/exercises")
            .RequireAuthorization();

        routeGroup.MapGet(string.Empty, ExerciseEndpoints.HandleGet);

        return routeGroup.WithTags("Exercise");
    }
}
