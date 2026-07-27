using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using FGScanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FGScanner.Database
{
    public class Dbcontext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<ActualInventory> ActualInventories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<RackLocation> RackLocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Here is where we inject your brand new connection logic!
                string connectionString = db_connection.GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateInventory_clean"))
                .HasOne(t => t.Shipment)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.controlNumber)
                .HasPrincipalKey(s => s.TransactionID);

            modelBuilder.Entity<Transaction>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateInventory_clean"))
                .HasOne(t => t.Returns)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.controlNumber)
                .HasPrincipalKey(s => s.TransactionID);
        }
    }
}
