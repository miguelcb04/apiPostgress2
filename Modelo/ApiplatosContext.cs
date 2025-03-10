using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPostgress.Modelo;

public partial class ApiplatosContext : DbContext
{
    public ApiplatosContext()
    {
    }

    public ApiplatosContext(DbContextOptions<ApiplatosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plato> Platos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-empty-heart-a8prhguo-pooler.eastus2.azure.neon.tech;Database=APIplatos;Username=neondb_owner;Password=npg_28deTiDamvVy");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("platos_pkey");

            entity.ToTable("platos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calorias).HasColumnName("calorias");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Disponible).HasColumnName("disponible");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Preparacion).HasColumnName("preparacion");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
