using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Biblioteca_Shared
{
    public class LibroDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio.")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "El número de páginas es obligatorio.")]
        [DisplayName("Número de páginas")]
        public int NumPaginas { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Este campo debe contener una fecha válida.")]
        public DateOnly FechaPublicacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public bool Disponible { get; set; }

        [Required(ErrorMessage = "El autor del libro es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un autor válido.")]
        public int IdAutor { get; set; }
        public AutorDTO? Autor { get; set; }
    }
}
