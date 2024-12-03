namespace GymApp.Identity.Services;

public interface IUserRegistrationHandler<in TUser>
{
    Task OnUserRegister(TUser user, UserRole role);
}
