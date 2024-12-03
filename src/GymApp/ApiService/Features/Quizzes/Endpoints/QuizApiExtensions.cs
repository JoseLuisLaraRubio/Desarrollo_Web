namespace GymApp.ApiService.Features.Quizzes.Endpoints;

public static class QuizApiExtensions
{
    public static IEndpointConventionBuilder MapQuizApi(
        this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/quiz")
            .RequireAuthorization();

        routeGroup.MapGet(string.Empty, QuizEndpoints.HandleGet);

        routeGroup.MapPost(string.Empty, QuizEndpoints.HandlePost);

        return routeGroup.WithTags("Quiz");
    }
}
