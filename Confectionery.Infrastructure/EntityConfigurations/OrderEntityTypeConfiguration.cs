using Confectionery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confectionery.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id).HasName("PK_Order");

            builder.Property(b => b.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(o => o.UnitPrice)
                .IsRequired();

            builder.Property(o => o.Quentity)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired();
            
            builder.Property(o => o.CreatedDtm)
                .IsRequired();

            builder.HasOne(c => c.Client)
                .WithMany(p => p.Orders)
                .HasForeignKey(c => c.ClientId)
                .HasConstraintName("FK_Order_Client")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Confection)
                .WithMany(p => p.Orders)
                .HasForeignKey(c => c.ConfectionId)
                .HasConstraintName("FK_Order_Confection")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
