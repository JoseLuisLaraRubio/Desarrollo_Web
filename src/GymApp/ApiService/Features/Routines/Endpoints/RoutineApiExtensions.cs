﻿namespace GymApp.ApiService.Features.Routines.Endpoints;

using GymApp.ApiService.Features.Workouts.Endpoints;

public static class RoutineApiExtensions
{
    public static IEndpointConventionBuilder MapRoutineApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/routines")
            .RequireAuthorization();

        routeGroup.MapGet("/current", RoutineEndpoints.HandleGet);

        routeGroup.MapPut("/current", RoutineEndpoints.HandlePut);

        routeGroup.MapWorkoutApi();

        return routeGroup.WithTags("Routine");
    }
}
