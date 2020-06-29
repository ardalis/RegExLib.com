using RegExLib.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RegExLib.Infrastructure.Data.Config
{
    public class ExpressionConfiguration : IEntityTypeConfiguration<Expression>
    {
        public void Configure(EntityTypeBuilder<Expression> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired();
            builder.Property(t => t.Pattern)
                .IsRequired();
            builder.Property(t => t.Description)
                .IsRequired();
        }
    }
}
