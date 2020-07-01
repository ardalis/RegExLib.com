using RegExLib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RegExLib.Web.Seeds
{
  public static class SeedData
  {

    public static void Initialize(IServiceProvider serviceProvider)
    {
      using var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);

      new ToDoItemsSeed(dbContext).PopulateTestData();
      new AuthorsSeed(dbContext).PopulateTestData();
      new ExpressionsSeed(dbContext).PopulateTestData();
    }

  }
}
