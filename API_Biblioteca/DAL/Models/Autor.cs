using System;
using System.Collections.Generic;

namespace API_Biblioteca.DAL.Models;

public partial class Autor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int IdTipoDocto { get; set; }

    public int NumDocto { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public string? Bibliografia { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual TipoDocto? IdTipoDoctoNavigation { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
