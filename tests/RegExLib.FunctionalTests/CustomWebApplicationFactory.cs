using RegExLib.Infrastructure.Data;
using RegExLib.UnitTests;
using RegExLib.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.TestHost;
using System.Linq;
using MediatR;
using RegExLib.Web.Seeds;

namespace RegExLib.FunctionalTests
{
  public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder
          .UseSolutionRelativeContentRoot("src/RegExLib.Web")
          .ConfigureServices(services =>
      {
              // Remove the app's ApplicationDbContext registration.
              var descriptor = services.SingleOrDefault(
                  d => d.ServiceType ==
                      typeof(DbContextOptions<AppDbContext>));

        if (descriptor != null)
        {
          services.Remove(descriptor);
        }

              // Add ApplicationDbContext using an in-memory database for testing.
              services.AddDbContext<AppDbContext>(options =>
              {
            options.UseInMemoryDatabase("InMemoryDbForTesting");
          });

              //// Create a new service provider.
              //var serviceProvider = new ServiceCollection()
              //    .AddEntityFrameworkInMemoryDatabase()
              //    .BuildServiceProvider();

              //// Add a database context (AppDbContext) using an in-memory
              //// database for testing.
              //services.AddDbContext<AppDbContext>(options =>
              //{
              //    options.UseInMemoryDatabase("InMemoryDbForTesting");
              //    options.UseInternalServiceProvider(serviceProvider);
              //});

              services.AddScoped<IMediator, NoOpMediator>();

              // Build the service provider.
              var sp = services.BuildServiceProvider();

              // Create a scope to obtain a reference to the database
              // context (AppDbContext).
              using (var scope = sp.CreateScope())
        {
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<AppDbContext>();

          var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

          try
          {
                  // Seed the database with test data.
                  new ToDoItemsSeed(db).PopulateTestData();
            new AuthorsSeed(db).PopulateTestData();
            new ExpressionsSeed(db).PopulateTestData();
          }
          catch (Exception ex)
          {
            logger.LogError(ex, "An error occurred seeding the " +
                      $"database with test messages. Error: {ex.Message}");
          }
        }
      });
    }
  }
}
