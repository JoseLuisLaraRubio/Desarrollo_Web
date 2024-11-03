namespace GymApp.Identity.Data;

public sealed class LoginRequest
{
    public required string UserName { get; init; }

    public required string Password { get; init; }
}
