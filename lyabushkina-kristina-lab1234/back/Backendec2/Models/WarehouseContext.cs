using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backendec2.Models;

/// <summary>
/// Контекст базы данных для управления складской системой.  
/// Обеспечивает взаимодействие с таблицами: ячеек (Cells), фабрик (Factories), товаров (Products) и поставок (Supplies).  
/// Используется для выполнения CRUD-операций, управления связями между сущностями и настройки схемы БД.  
/// </summary>

public partial class WarehouseContext : DbContext
{
    public WarehouseContext()
    {
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cell> Cells { get; set; }

    public virtual DbSet<Factory> Factories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\Andrew\\source\\repos\\Backendec2\\Backendec2\\warehouse.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cell>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Cells_Id").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("productId");

            entity.HasOne(d => d.Product).WithMany(p => p.Cells).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Factory>(entity =>
        {
            entity.ToTable("Factory");

            entity.HasIndex(e => e.Id, "IX_Factory_Id").IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Products_Id").IsUnique();

            entity.Property(e => e.CellId).HasColumnName("cellId");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.ToTable("Supply");

            entity.HasIndex(e => e.Id, "IX_Supply_Id").IsUnique();

            entity.Property(e => e.FactoryId).HasColumnName("factoryId");
            entity.Property(e => e.ProductId).HasColumnName("productId");

            entity.HasOne(d => d.Product).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
