﻿using Confectionery.Domain.Entities;
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
        public DbSet<ConfectionPicture> ConfectionPictures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConfectionPictureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
        }
    }
}
