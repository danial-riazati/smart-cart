using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class CartDBContext : DbContext
    {
        public CartDBContext()
        {
        }

        public CartDBContext(DbContextOptions<CartDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CheckInOut> CheckInOuts { get; set; }
        public virtual DbSet<Factor> Factors { get; set; }
        public virtual DbSet<FailureReport> FailureReports { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=127.0.0.1;Port=3307;Database=CartDB;Uid=user;Password=C@rtDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.HasIndex(e => e.StoreId, "fk_Cart_Store1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(45)
                    .HasColumnName("appVersion");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.InUse).HasColumnName("inUse");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.StoreType).HasColumnName("storeType");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Cart_Store1");
            });

            modelBuilder.Entity<CheckInOut>(entity =>
            {
                entity.ToTable("CheckInOut");

                entity.HasIndex(e => e.FactorId, "fk_CheckInOut_Factor1_idx");

                entity.HasIndex(e => e.CartId, "fk_CheckIn_Cart_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Browser)
                    .HasMaxLength(45)
                    .HasColumnName("browser");

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.CheckinDate)
                    .HasColumnType("date")
                    .HasColumnName("checkinDate");

                entity.Property(e => e.CheckoutDate)
                    .HasColumnType("date")
                    .HasColumnName("checkoutDate");

                entity.Property(e => e.FactorId).HasColumnName("factorId");

                entity.Property(e => e.IsCheckedOut).HasColumnName("isCheckedOut");

                entity.Property(e => e.IsFailed).HasColumnName("isFailed");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("phoneNumber");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CheckInOuts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CheckIn_Cart");

                entity.HasOne(d => d.Factor)
                    .WithMany(p => p.CheckInOuts)
                    .HasForeignKey(d => d.FactorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CheckInOut_Factor1");
            });

            modelBuilder.Entity<Factor>(entity =>
            {
                entity.ToTable("Factor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.IsSucceed).HasColumnName("isSucceed");
            });

            modelBuilder.Entity<FailureReport>(entity =>
            {
                entity.ToTable("FailureReport");

                entity.HasIndex(e => e.CartId, "fk_FailureReport_Cart1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");

                entity.Property(e => e.FixedDate)
                    .HasColumnType("date")
                    .HasColumnName("fixedDate");

                entity.Property(e => e.IsFixed).HasColumnName("isFixed");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.FailureReports)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_FailureReport_Cart1");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IsPublished).HasColumnName("isPublished");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .HasColumnName("address");

                entity.Property(e => e.CityName)
                    .HasMaxLength(45)
                    .HasColumnName("cityName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(45)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(45)
                    .HasColumnName("provinceName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
