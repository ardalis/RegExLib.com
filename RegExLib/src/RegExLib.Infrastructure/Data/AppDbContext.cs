using Microsoft.EntityFrameworkCore;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel;
using Ardalis.EFCore.Extensions;
using MediatR;

namespace RegExLib.Infrastructure.Data
{
  public class AppDbContext : DbContext
  {
    private readonly IMediator? _mediator;

    //public AppDbContext(DbContextOptions options) : base(options)
    //{
    //}
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
            : base(options)
    {
      _mediator = mediator;
    }

    public DbSet<Expression> Expression { get; set; } = null!;
    public DbSet<Author> Author { get; set; } = null!;
    public DbSet<Reaction> Reaction { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

      // ignore events if no dispatcher provided
      if (_mediator == null) return result;

      // dispatch events only if save was successful
      var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<object>>()
          .Select(e => e.Entity)
          .Where(e => e.Events.Any())
          .ToArray();

      foreach (var entity in entitiesWithEvents)
      {
        var events = entity.Events.ToArray();
        entity.Events.Clear();
        foreach (var domainEvent in events)
        {
          await _mediator.Publish(domainEvent).ConfigureAwait(false);
        }
      }

      return result;
    }

    public override int SaveChanges()
    {
      return SaveChangesAsync().GetAwaiter().GetResult();
    }
  }
}
