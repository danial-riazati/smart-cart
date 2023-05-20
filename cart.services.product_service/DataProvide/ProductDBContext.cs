using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace cart.services.product_service.DataProvide
{
    public partial class ProductDBContext : DbContext
    {
        public ProductDBContext()
        {
        }

        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<SmartCartinfo> SmartCartinfos { get; set; }
        public virtual DbSet<Version> Versions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=10.51.10.137;Port=3306;Database=ProductDB;Uid=user;Pwd=SM@RTcart");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id");

                entity.Property(e => e.Bytes)
                    .HasMaxLength(255)
                    .HasColumnName("bytes");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasMaxLength(45)
                    .HasColumnName("price");
            });

            modelBuilder.Entity<SmartCartinfo>(entity =>
            {
                entity.ToTable("SmartCartinfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataName).HasMaxLength(200);

                entity.Property(e => e.DataValue).HasMaxLength(200);
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.HasKey(e => e.VersionName)
                    .HasName("PRIMARY");

                entity.ToTable("Version");

                entity.Property(e => e.VersionName).HasMaxLength(45);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .HasColumnName("url");

                entity.Property(e => e.VersionNumber)
                    .HasMaxLength(45)
                    .HasColumnName("versionNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
