namespace GymApp.Database;

using MySqlConnector;

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
                _ => TimeSpan.FromSeconds(10));

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
            catch (InvalidOperationException exception) when (DatabaseNotReadyExceptionFilter(exception))
            {
                var exceptionWrap = new ServiceUnavailableException("Database not ready.", exception);
                onException.Invoke(exceptionWrap);
                throw exceptionWrap;
            }
        });
    }

    private static bool DatabaseNotReadyExceptionFilter(InvalidOperationException exception)
    {
        return exception.InnerException is MySqlException mySqlException
            && mySqlException.ErrorCode == MySqlErrorCode.UnableToConnectToHost
            && mySqlException.InnerException is MySqlEndOfStreamException;
    }
}
