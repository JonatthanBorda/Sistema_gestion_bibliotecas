using System;
using System.Collections.Generic;

namespace API_Biblioteca.DAL.Models;

public partial class TipoDocto
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();
}
