using RegExLib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MediatR;

namespace RegExLib.IntegrationTests.Data
{
    public abstract class BaseEfRepoTestFixture
    {
#nullable disable
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected AppDbContext _dbContext;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("RegExLib")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockMediator = new Mock<IMediator>();

            _dbContext = new AppDbContext(options, mockMediator.Object);
            return new EfRepository(_dbContext);
        }
    }
}
