namespace GymApp.Database;

using Npgsql;

using Polly;
using Polly.Retry;

using RaptorUtils.Exceptions;

// TODO: Replace with aspire 9 WaitFor
public static class AppDbContextInitializer
{
    public static async Task<bool> EnsureCreatedAsync(AppDbContext context, Action<Exception> onException)
    {
        var retryPolicy = Policy
            .Handle<ServiceUnavailableException>()
            .WaitAndRetryAsync(
                retryCount: 5,
                _ => TimeSpan.FromSeconds(5));

        PolicyResult<bool> result = await EnsureCreatedAsync(context, onException, retryPolicy);

        if (result.Outcome == OutcomeType.Failure)
        {
            throw new TimeoutException("Could not create database.");
        }

        return result.Result;
    }

    private static Task<PolicyResult<bool>> EnsureCreatedAsync(
        AppDbContext context,
        Action<Exception> onException,
        AsyncRetryPolicy retryPolicy)
    {
        return retryPolicy.ExecuteAndCaptureAsync(async () =>
        {
            try
            {
                return await context.Database.EnsureCreatedAsync();
            }
            catch (NpgsqlException exception) when (DatabaseNotReadyExceptionFilter(exception))
            {
                var exceptionWrap = new ServiceUnavailableException("Database not ready.", exception);
                onException.Invoke(exceptionWrap);
                throw exceptionWrap;
            }
        });
    }

    private static bool DatabaseNotReadyExceptionFilter(NpgsqlException exception)
    {
        if (exception is PostgresException postgresException)
        {
            return postgresException.SqlState == "57P03";
        }

        return exception.InnerException is EndOfStreamException;
    }
}
