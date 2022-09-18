using RegExLib.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RegExLib.Infrastructure.Data.Config
{
  public class AuthorConfiguration : IEntityTypeConfiguration<Author>
  {
    public void Configure(EntityTypeBuilder<Author> builder)
    {
      builder.Property(t => t.UserId)
          .IsRequired();
      builder.Property(t => t.Username)
          .IsRequired();
      builder.Property(t => t.FullName)
          .IsRequired();
    }
  }
}
