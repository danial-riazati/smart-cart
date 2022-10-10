using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace cart.services.invoice_service.DataProvide
{
    public partial class InvoiceDBContext : DbContext
    {
        public InvoiceDBContext()
        {
        }

        public InvoiceDBContext(DbContextOptions<InvoiceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.IsSucceed).HasColumnName("isSucceed");

                entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
