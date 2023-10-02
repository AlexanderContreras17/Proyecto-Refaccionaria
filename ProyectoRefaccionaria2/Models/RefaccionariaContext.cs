using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRefaccionaria2.Models;

public partial class RefaccionariaContext : DbContext
{
    public RefaccionariaContext()
    {
    }

    public RefaccionariaContext(DbContextOptions<RefaccionariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Marcas> Marcas { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Rolesusuarios> Rolesusuarios { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<VwMarcasconstock> VwMarcasconstock { get; set; }

    public virtual DbSet<VwMarcasordenadas> VwMarcasordenadas { get; set; }

    public virtual DbSet<VwProductosordenados> VwProductosordenados { get; set; }

    public virtual DbSet<VwTodaslasmarcas> VwTodaslasmarcas { get; set; }

    public virtual DbSet<VwTodoslosproductos> VwTodoslosproductos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=refaccionaria;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Marcas>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PRIMARY");

            entity.ToTable("marcas");

            entity.Property(e => e.CantProductos).HasDefaultValueSql("'0'");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.IdMarcaP, "productos_ibfk_1");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Precio).HasPrecision(8, 2);

            entity.HasOne(d => d.IdMarcaPNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarcaP)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("productos_ibfk_1");
        });

        modelBuilder.Entity<Rolesusuarios>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rolesusuarios");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.IdTipoRol, "IdTipoRol");

            entity.Property(e => e.Contraseña).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(60);
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdTipoRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoRol)
                .HasConstraintName("usuarios_ibfk_1");
        });

        modelBuilder.Entity<VwMarcasconstock>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_marcasconstock");

            entity.Property(e => e.CantProductos).HasDefaultValueSql("'0'");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<VwMarcasordenadas>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_marcasordenadas");

            entity.Property(e => e.CantProductos).HasDefaultValueSql("'0'");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<VwProductosordenados>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_productosordenados");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Precio).HasPrecision(8, 2);
        });

        modelBuilder.Entity<VwTodaslasmarcas>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_todaslasmarcas");

            entity.Property(e => e.CantProductos).HasDefaultValueSql("'0'");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<VwTodoslosproductos>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_todoslosproductos");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Precio).HasPrecision(8, 2);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
