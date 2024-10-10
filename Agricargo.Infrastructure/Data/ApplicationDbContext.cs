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
        public DbSet<Trip> Trips { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>()
                .HasMany(s => s.Trips)
                .WithOne(t => t.Ship)
                .HasForeignKey(t => t.ShipId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
