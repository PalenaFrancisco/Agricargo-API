﻿// <auto-generated />
using System;
using Agricargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agricargo.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Agricargo.Domain.Entities.Ship", b =>
                {
                    b.Property<int>("ShipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Available")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Capacity")
                        .HasColumnType("REAL");

                    b.Property<string>("Captain")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeShip")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ShipId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArriveDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Destiny")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFullCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Origin")
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ShipId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TripState")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShipId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeUser")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("TypeUser").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Client", b =>
                {
                    b.HasBaseType("Agricargo.Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Company", b =>
                {
                    b.HasBaseType("Agricargo.Domain.Entities.User");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.SuperAdmin", b =>
                {
                    b.HasBaseType("Agricargo.Domain.Entities.User");

                    b.HasDiscriminator().HasValue("SuperAdmin");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Ship", b =>
                {
                    b.HasOne("Agricargo.Domain.Entities.Company", "Company")
                        .WithMany("Ships")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Trip", b =>
                {
                    b.HasOne("Agricargo.Domain.Entities.Ship", "Ship")
                        .WithMany("TripList")
                        .HasForeignKey("ShipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ship");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Ship", b =>
                {
                    b.Navigation("TripList");
                });

            modelBuilder.Entity("Agricargo.Domain.Entities.Company", b =>
                {
                    b.Navigation("Ships");
                });
#pragma warning restore 612, 618
        }
    }
}
