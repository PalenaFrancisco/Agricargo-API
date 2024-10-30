using Agricargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>().HasKey(e => e.Id);

            modelBuilder.Entity<Ship>()
                .HasOne(s => s.Company)
                .WithMany(c => c.Ships)
                .HasForeignKey(s => s.CompanyId);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("TypeUser")
                .HasValue<Client>("Client")
                .HasValue<Company>("Admin")
                .HasValue<SuperAdmin>("SuperAdmin");


            modelBuilder.Entity<Trip>().HasKey(e => e.Id);


            modelBuilder.Entity<Trip>()
               .HasOne(t => t.Ship)
               .WithMany(s => s.Trips)
               .HasForeignKey(t => t.ShipId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Trip)
            .WithMany()
            .HasForeignKey(f => f.TripId);


            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Client)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.ClientId);

            modelBuilder.Entity<SuperAdmin>().HasData(CreateSuperAdmins());
            modelBuilder.Entity<Company>().HasData(CreateCompanies());
            modelBuilder.Entity<Client>().HasData(CreateClients());

            base.OnModelCreating(modelBuilder);
        }
        private SuperAdmin[] CreateSuperAdmins()
        {
            return new SuperAdmin[]
            {
                new SuperAdmin
                {
                    Id = Guid.NewGuid(),
                    Name = "web master",
                    Email = "sys_admin@gmail.com",
                    Phone = "1242214",
                    TypeUser = "SuperAdmin",
                    Password = BCrypt.Net.BCrypt.HashPassword("superadmin1234")
                }
            };
        }

        private Company[] CreateCompanies()
        {
            return new Company[]
            {
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Mario Massonnat",
                    Email = "mario@gmail.com",
                    Phone = "153252",
                    TypeUser = "Admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("mario1234"),
                    CompanyName = "El Maruco CIA"
                },
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Francisco Palena",
                    Email = "pale@gmail.com",
                    Phone = "1986",
                    TypeUser = "Admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("pale1234"),
                    CompanyName = "Pale SRL"
                }
            };
        }

        private Client[] CreateClients()
        {
            return new Client[]
            {
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Pablo",
                    Email = "pablo@gmail.com",
                    Phone = "19864343",
                    TypeUser = "Client",
                    Password = BCrypt.Net.BCrypt.HashPassword("pablo1234")
                },
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Emiliano",
                    Email = "emi@gmail.com",
                    Phone = "1923486",
                    TypeUser = "Client",
                    Password = BCrypt.Net.BCrypt.HashPassword("emi1234")
                }
            };
        }
    }
}