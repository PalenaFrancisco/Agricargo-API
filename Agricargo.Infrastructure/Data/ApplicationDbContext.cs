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

            base.OnModelCreating(modelBuilder);
        }
    }
}
