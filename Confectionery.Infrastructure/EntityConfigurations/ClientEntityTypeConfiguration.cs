using Confectionery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confectionery.Infrastructure.EntityConfigurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(o => o.Id).HasName("PK_Client");

            builder.Property(b => b.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(o => o.FullName)
                .HasMaxLength(550)
                .IsRequired();

            builder.Property(o => o.Email)
                .HasMaxLength(125)
                .IsRequired();

            builder.Property(o => o.InstagramProfile)
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(o => o.MobileNumber)
                .HasMaxLength(10)
                .IsRequired();

            builder.ToTable(o => 
                o.HasCheckConstraint("CK_Client_MobileNumber", "[MobileNumber] NOT LIKE '%[^0-9]%'")
            );
        }
    }
}
