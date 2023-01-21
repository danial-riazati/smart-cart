using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace cart.services.account_service.DataProvide
{
    public partial class AccountDBContext : DbContext
    {
        public AccountDBContext()
        {
        }

        public AccountDBContext(DbContextOptions<AccountDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Checkin> Checkins { get; set; }
        public virtual DbSet<Checkout> Checkouts { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkin>(entity =>
            {
                entity.ToTable("Checkin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.IsCheckedOut).HasColumnName("isCheckedOut");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.ToTable("Checkout");

                entity.HasIndex(e => e.CheckinId, "fk_Checkout_Checkin_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckinId).HasColumnName("checkinId");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.InvoiceId).HasColumnName("invoiceId");

                entity.Property(e => e.PayementId).HasColumnName("payementId");

                entity.Property(e => e.TotalShopValue).HasColumnName("totalShopValue");

                entity.HasOne(d => d.Checkin)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.CheckinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Checkout_Checkin");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
