using Confectionery.Domain.Entities;
using Confectionery.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Confectionery.Infrastructure
{
    public class СonfectioneryContext: DbContext
    {
        public СonfectioneryContext(DbContextOptions<СonfectioneryContext> options) 
            : base(options)
        {
        }

        public DbSet<Confection> Confections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConfectionPictureEntityTypeConfiguration());
        }
    }
}
