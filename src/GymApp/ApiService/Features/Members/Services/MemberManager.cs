namespace GymApp.ApiService.Features.Members.Services;

using System.Threading.Tasks;

using GymApp.Database;
using GymApp.Database.Entities;
using GymApp.Identity;
using GymApp.Identity.Services;

public sealed class MemberManager(
    AppDbContext dbContext) : IUserRegistrationHandler<AppUser>
{
    Task IUserRegistrationHandler<AppUser>.OnUserRegister(AppUser user, UserRole role)
    {
        if (role != UserRole.Member)
        {
            return Task.CompletedTask;
        }

        var member = new Member()
        {
            User = user,
        };

        dbContext.Members.Add(member);
        return dbContext.SaveChangesAsync();
    }

    public IQueryable<Member> Query(AppUser user)
    {
        return dbContext.Members
            .Where(m => m.User.Id == user.Id);
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
