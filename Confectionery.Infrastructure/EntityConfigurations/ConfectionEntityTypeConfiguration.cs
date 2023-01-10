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

            builder.Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(o => o.Price)
                .IsRequired();

            builder.Property(o => o.Type)
                .IsRequired();

            builder.Property(o => o.Weight)
                .IsRequired();

            builder.Property(o => o.MinimumOrderCount)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(o => o.IsOutOfStock)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
