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
        }
    }
}
