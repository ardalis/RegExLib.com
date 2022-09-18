using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegExLib.Core.Constants;

namespace RegExLib.Infrastructure.Identity
{
  public class AppIdentityDbContextSeed
  {
    public static async Task SeedAsync(AppIdentityDbContext identityDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      if (identityDbContext.Database.IsSqlServer())
      {
        await identityDbContext.Database.MigrateAsync();
      }

      await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATORS));

      string defaultUserName = "demouser@regexlib.test";
      var defaultUser = new ApplicationUser { UserName = defaultUserName, Email = defaultUserName };
      await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

      string adminUserName = "admin@regexlib.test";
      var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
      await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
      adminUser = await userManager.FindByNameAsync(adminUserName);
      await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATORS);
    }
  }
}
