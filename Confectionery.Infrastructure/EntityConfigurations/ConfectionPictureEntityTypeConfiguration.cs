using Confectionery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confectionery.Infrastructure.EntityConfigurations
{
    public class ConfectionPictureEntityTypeConfiguration : IEntityTypeConfiguration<ConfectionPicture>
    {
        public void Configure(EntityTypeBuilder<ConfectionPicture> builder)
        {
            builder.ToTable("ConfectionPicture");

            builder.HasKey(o => o.Id).HasName("PK_ConfectionPicture");

            builder.Property(o => o.ShortName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.Extension)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Content)
                .IsRequired();

            builder.HasOne(c => c.Confection)
                .WithMany(p => p.Pictures)
                .HasForeignKey(c => c.Id)
                .HasConstraintName("FK_ConfectionPicture_Confection")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
