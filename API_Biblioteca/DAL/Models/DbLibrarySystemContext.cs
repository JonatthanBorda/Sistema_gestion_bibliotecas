using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.DAL.Models;

public partial class DbLibrarySystemContext : DbContext
{
    public DbLibrarySystemContext()
    {
    }

    public DbLibrarySystemContext(DbContextOptions<DbLibrarySystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<TipoDocto> TipoDoctos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autor__3214EC0706C7FCCA");

            entity.ToTable("Autor");

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(255);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(255);

            entity.HasOne(d => d.IdTipoDoctoNavigation).WithMany(p => p.Autors)
                .HasForeignKey(d => d.IdTipoDocto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Autor__IdTipoDoc__267ABA7A");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libro__3214EC074A03F95E");

            entity.ToTable("Libro");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(255);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(255);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(255);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libro__IdAutor__29572725");
        });

        modelBuilder.Entity<TipoDocto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoDoct__3214EC07C8EDAE96");

            entity.ToTable("TipoDocto");

            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
