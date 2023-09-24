using Confectionery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confectionery.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(o => o.Id).HasName("PK_User");

            builder.Property(b => b.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(o => o.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Surname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Email)
                .HasMaxLength(125)
                .IsRequired();

            builder.Property(o => o.PasswordHash)
                .IsRequired();

            builder.Property(o => o.Role)
                .IsRequired();
        }
    }
}
