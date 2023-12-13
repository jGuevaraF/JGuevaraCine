using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JguevaraCineContext : DbContext
{
    public JguevaraCineContext()
    {
    }

    public JguevaraCineContext(DbContextOptions<JguevaraCineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cine> Cines { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ALIEN71; Database= JGuevaraCine; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cine>(entity =>
        {
            entity.HasKey(e => e.IdCine).HasName("PK__Cine__394B724B80E5D922");

            entity.ToTable("Cine");

            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Latitud)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Longitud)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ZonaNombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ventas).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Cines)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("FK__Cine__IdZona__1273C1CD");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07D6A73BE2");

            entity.ToTable("Producto");

            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97470F06D0");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053485372D29").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(20);
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PK__Zona__F631C12D1AAC90D0");

            entity.ToTable("Zona");

            entity.Property(e => e.Nombre)
                .HasMaxLength(7)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
