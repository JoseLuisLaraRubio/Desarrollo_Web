namespace GymApp.Identity.Services;

using Microsoft.Extensions.Configuration;

using RaptorUtils.Extensions.Configuration;

public class AdminCodeValidator(IConfiguration configuration)
{
    public bool IsValid(string code)
    {
        return configuration.GetRequired("AdminCode") == code;
    }
}
