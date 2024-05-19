﻿// <auto-generated />
using System;
using API_Biblioteca.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_Biblioteca.Migrations
{
    [DbContext(typeof(DbLibrarySystemContext))]
    [Migration("20240519051736_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_Biblioteca.DAL.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Bibliografia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<DateOnly>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<int>("IdTipoDocto")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumDocto")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Autor__3214EC0706C7FCCA");

                    b.HasIndex("IdTipoDocto");

                    b.ToTable("Autor", (string)null);
                });

            modelBuilder.Entity("API_Biblioteca.DAL.Models.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Disponible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<DateOnly>("FechaPublicacion")
                        .HasColumnType("date");

                    b.Property<int>("IdAutor")
                        .HasColumnType("int");

                    b.Property<int>("NumPaginas")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Libro__3214EC074A03F95E");

                    b.HasIndex("IdAutor");

                    b.ToTable("Libro", (string)null);
                });

            modelBuilder.Entity("API_Biblioteca.DAL.Models.TipoDocto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__TipoDoct__3214EC07C8EDAE96");

                    b.ToTable("TipoDocto", (string)null);
                });

            modelBuilder.Entity("API_Biblioteca.DAL.Models.Autor", b =>
                {
                    b.HasOne("API_Biblioteca.DAL.Models.TipoDocto", "IdTipoDoctoNavigation")
                        .WithMany("Autors")
                        .HasForeignKey("IdTipoDocto")
                        .IsRequired()
                        .HasConstraintName("FK__Autor__IdTipoDoc__267ABA7A");

                    b.Navigation("IdTipoDoctoNavigation");
                });

            modelBuilder.Entity("API_Biblioteca.DAL.Models.Libro", b =>
                {
                    b.HasOne("API_Biblioteca.DAL.Models.Autor", "IdAutorNavigation")
                        .WithMany("Libros")
                        .HasForeignKey("IdAutor")
                        .IsRequired()
                        .HasConstraintName("FK__Libro__IdAutor__29572725");

                    b.Navigation("IdAutorNavigation");
                });

            modelBuilder.Entity("API_Biblioteca.DAL.Models.Autor", b =>
                {
                    b.Navigation("Libros");
                });

            modelBuilder.Entity("API_Biblioteca.DAL.Models.TipoDocto", b =>
                {
                    b.Navigation("Autors");
                });
#pragma warning restore 612, 618
        }
    }
}
