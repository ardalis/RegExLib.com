using Autofac.Extensions.DependencyInjection;
using RegExLib.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Identity;
using RegExLib.Infrastructure.Identity;
using System.Threading.Tasks;
using RegExLib.Web.Seeds;

namespace RegExLib.Web
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;

        try
        {
          var context = services.GetRequiredService<AppDbContext>();
          //                    context.Database.Migrate();
          context.Database.EnsureCreated();
          SeedData.Initialize(services);

          var identityContext = services.GetRequiredService<AppIdentityDbContext>();
          identityContext.Database.EnsureCreated();

          var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
          var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
          await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred seeding the DB.");
        }
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webBuilder =>
    {
      webBuilder
              .UseStartup<Startup>()
              .ConfigureLogging(logging =>
          {
          logging.ClearProviders();
          logging.AddConsole();
          logging.AddAzureWebAppDiagnostics(); // add this if deploying to Azure
            });
    });

  }
}
