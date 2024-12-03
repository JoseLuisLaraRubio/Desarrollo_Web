namespace GymApp.Identity;

using Microsoft.AspNetCore.Identity;

public abstract class AppIdentityUser : IdentityUser
{
    public new string UserName
    {
        get => base.UserName
            ?? throw new InvalidOperationException("The username field was expected to have a value.");
        set => base.UserName = value;
    }
}
