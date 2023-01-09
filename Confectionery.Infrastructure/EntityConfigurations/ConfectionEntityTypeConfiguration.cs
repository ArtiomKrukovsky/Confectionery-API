using Confectionery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confectionery.Infrastructure.EntityConfigurations
{
    public class ConfectionEntityTypeConfiguration : IEntityTypeConfiguration<Confection>
    {
        public void Configure(EntityTypeBuilder<Confection> builder)
        {
            builder.ToTable("Confection");

            builder.HasKey(o => o.Id).HasName("PK_Confection");

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.MinimumOrderCount)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(x => x.isOutOfStock)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
