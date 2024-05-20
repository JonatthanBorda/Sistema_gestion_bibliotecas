using System;
using System.Collections.Generic;

namespace API_Biblioteca.DAL.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int NumPaginas { get; set; }

    public DateOnly FechaPublicacion { get; set; }

    public bool Disponible { get; set; }

    public int IdAutor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Autor? IdAutorNavigation { get; set; } = null!;
}
