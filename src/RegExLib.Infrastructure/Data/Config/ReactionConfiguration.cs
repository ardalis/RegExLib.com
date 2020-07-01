using RegExLib.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RegExLib.Infrastructure.Data.Config
{
  public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
  {
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
      builder.Property(t => t.ExpressionId)
          .IsRequired();
      builder.Property(t => t.DateLastUpdated)
        .IsRequired();
      builder.Property(t => t.ReactionType)
        .IsRequired();
    }
  }
}
