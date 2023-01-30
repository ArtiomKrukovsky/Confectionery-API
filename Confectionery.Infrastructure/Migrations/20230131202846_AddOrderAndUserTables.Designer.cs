﻿// <auto-generated />
using System;
using Confectionery.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Confectionery.Infrastructure.Migrations
{
    [DbContext(typeof(СonfectioneryContext))]
    [Migration("20230131202846_AddOrderAndUserTables")]
    partial class AddOrderAndUserTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Confectionery.Domain.Entities.Confection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsOutOfStock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("MinimumOrderCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id")
                        .HasName("PK_Confection");

                    b.ToTable("Confection", (string)null);
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.ConfectionPicture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("ConfectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2050)
                        .HasColumnType("nvarchar(2050)");

                    b.HasKey("Id")
                        .HasName("PK_ConfectionPicture");

                    b.HasIndex("ConfectionId");

                    b.ToTable("ConfectionPicture", (string)null);
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("ConfectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDtm")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quentity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_Order");

                    b.HasIndex("ConfectionId");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(125)
                        .HasColumnType("nvarchar(125)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)");

                    b.Property<string>("InstagramProfile")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id")
                        .HasName("PK_User");

                    b.ToTable("User", null, t =>
                        {
                            t.HasCheckConstraint("CK_User_MobileNumber", "[MobileNumber] NOT LIKE '%[^0-9]%'");
                        });
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.ConfectionPicture", b =>
                {
                    b.HasOne("Confectionery.Domain.Entities.Confection", "Confection")
                        .WithMany("Pictures")
                        .HasForeignKey("ConfectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ConfectionPicture_Confection");

                    b.Navigation("Confection");
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.Order", b =>
                {
                    b.HasOne("Confectionery.Domain.Entities.Confection", "Confection")
                        .WithMany("Orders")
                        .HasForeignKey("ConfectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_Confection");

                    b.HasOne("Confectionery.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_User");

                    b.Navigation("Confection");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.Confection", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("Confectionery.Domain.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
