using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace csharp_dtc.API.OrderDetailDbContextModels;

public partial class OrderDetailDbContext : DbContext
{
    public OrderDetailDbContext(DbContextOptions<OrderDetailDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblOrderDetail>(entity =>
        {
            entity.ToTable("Tbl_Order_Detail");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNo).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
