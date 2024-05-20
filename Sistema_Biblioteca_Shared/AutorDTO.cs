using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Biblioteca_Shared
{
    public class AutorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un tipo de documento válido.")]
        public int IdTipoDocto { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        public int NumDocto { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Este campo debe contener una fecha válida.")]
        public DateOnly FechaNacimiento { get; set; }

        public string? Bibliografia { get; set; }

        public List<LibroDTO> Libros { get; set; } = new List<LibroDTO>();
    }
}
