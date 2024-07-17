using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockManagentSystem.Models;

public partial class StockManagementContext : DbContext
{
    public StockManagementContext()
    {
    }

    public StockManagementContext(DbContextOptions<StockManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B7925539E2");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).ValueGeneratedNever();

            entity.HasOne(d => d.PurchaseProduct).WithMany(p => p.Carts)
                .HasForeignKey(d => d.PurchaseProductId)
                .HasConstraintName("FK__Cart__PurchasePr__3C69FB99");

            entity.HasOne(d => d.Purchasecategory).WithMany(p => p.Carts)
                .HasForeignKey(d => d.PurchasecategoryId)
                .HasConstraintName("FK__Cart__Purchaseca__3D5E1FD2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Category__6A1C8AFA9A1369C5");

            entity.ToTable("Category");

            entity.Property(e => e.CatId).ValueGeneratedNever();
            entity.Property(e => e.CatName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProId).HasName("PK__Product__62029590825DC9B8");

            entity.ToTable("Product");

            entity.Property(e => e.ProId).ValueGeneratedNever();
            entity.Property(e => e.BrandName).HasMaxLength(50);
            entity.Property(e => e.ProName).HasMaxLength(50);
            entity.Property(e => e.ProPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
