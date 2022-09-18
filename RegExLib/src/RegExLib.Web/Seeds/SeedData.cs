using RegExLib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RegExLib.Infrastructure.Identity;

namespace RegExLib.Web.Seeds;
public class SeedData : ISeedData
{

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);

    new AuthorsSeed(dbContext).PopulateTestData();
    new ExpressionsSeed(dbContext).PopulateTestData();
  }
  public static async Task PopulateInitDataAsync(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
  {
  }
}

