namespace GymApp.Identity;

using Microsoft.AspNetCore.Authorization;

public static class AppPolicy
{
    public static class AdminRole
    {
        public const string Name = "admin-role";

        public static Action<AuthorizationPolicyBuilder> Create()
        {
            return b => b.RequireRole(nameof(UserRole.Admin));
        }
    }
}
