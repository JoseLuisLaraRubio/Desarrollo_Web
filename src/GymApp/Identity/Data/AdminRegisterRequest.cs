namespace GymApp.Identity.Data;

public sealed class AdminRegisterRequest : RegisterRequest
{
    public required string AdminCode { get; init; }
}
