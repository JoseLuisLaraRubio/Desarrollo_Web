namespace GymApp.ApiService.Features.Progress.Endpoints;

public static class ProgressApiExtensions
{
    public static IEndpointConventionBuilder MapProgressApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/routines");

        routeGroup.MapPost("{routineId}/progress", ProgressEndpoints.HandlePost);

        return routeGroup;
    }
}
