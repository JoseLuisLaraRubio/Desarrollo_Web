namespace GymApp.Database;

using GymApp.Database.Entities;
using GymApp.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RaptorUtils.AspNet.Identity;

public class AppDbContext(
    DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        SeedRoles(builder);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        var appNamespace = new Guid("f3cf2703-30de-4ca2-a10f-1f2f63a2972c");
        var roleGenerator = new IdentityRoleGenerator(appNamespace);

        IEnumerable<IdentityRole> roles = roleGenerator.Generate<UserRole>();

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
