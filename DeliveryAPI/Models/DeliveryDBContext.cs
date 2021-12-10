using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DeliveryAPI.Models
{
    public partial class DeliveryDBContext : DbContext
    {
        public DeliveryDBContext()
        {
        }

        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
               // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DeliveryDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Deliveries_CustomerId");

                entity.HasIndex(e => e.ShopId, "IX_Deliveries_ShopId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.ShopId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.DeliveryId, "IX_Products_DeliveryId");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DeliveryId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
