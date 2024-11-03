namespace GymApp.Identity.Data;

public class RegisterRequest
{
    public required string UserName { get; init; }

    public required string Password { get; init; }
}
