using Confectionery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confectionery.Infrastructure.EntityConfigurations
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(o => o.Id).HasName("PK_RefreshToken");

            builder.Property(b => b.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(o => o.Token)
                .IsRequired();

            builder.Property(o => o.ExpirationTime)
                .IsRequired();

            builder.HasOne(c => c.User)
                .WithOne(p => p.RefreshToken)
                .HasForeignKey<RefreshToken>(c => c.UserId)
                .HasConstraintName("FK_RefreshToken_User")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
