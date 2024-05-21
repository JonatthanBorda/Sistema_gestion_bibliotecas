using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;
using Sistema_Biblioteca_Shared;

namespace API_Biblioteca.BLL.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IUnitOfWork unitOfWork, ILogger<AuthorService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<AutorDTO>> GetAllAuthorsAsync()
        {
            _logger.LogInformation("Obteniendo todos los autores de la base de datos.");
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return authors.Select(author => new AutorDTO
            {
                Id = author.Id,
                Nombre = author.Nombre,
                Apellido = author.Apellido,
                IdTipoDocto = author.IdTipoDocto,
                NumDocto = author.NumDocto,
                FechaNacimiento = author.FechaNacimiento,
                Bibliografia = author.Bibliografia,
                Libros = author.Libros.Select(libro => new LibroDTO
                {
                    Id = libro.Id,
                    Titulo = libro.Titulo,
                    NumPaginas = libro.NumPaginas,
                    FechaPublicacion = libro.FechaPublicacion,
                    Disponible = libro.Disponible,
                    IdAutor = libro.IdAutor
                }).ToList()
            }).ToList();
        }


        public async Task<AutorDTO> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                return null;

            return new AutorDTO
            {
                Id = author.Id,
                Nombre = author.Nombre,
                Apellido = author.Apellido,
                IdTipoDocto = author.IdTipoDocto,
                NumDocto = author.NumDocto,
                FechaNacimiento = author.FechaNacimiento,
                Bibliografia = author.Bibliografia,
                Libros = author.Libros.Select(libro => new LibroDTO
                {
                    Id = libro.Id,
                    Titulo = libro.Titulo,
                    NumPaginas = libro.NumPaginas,
                    FechaPublicacion = libro.FechaPublicacion,
                    Disponible = libro.Disponible,
                    IdAutor = libro.IdAutor
                }).ToList()
            };
        }

        public async Task AddAuthorAsync(Autor author)
        {
            _logger.LogInformation("Agregando un nuevo autor a la base de datos.");

            // Se agregan los campos de auditoría por defecto
            author.FechaCreacion = DateTime.Now;
            author.UsuarioCreacion = "System";

            // Se verifica la unicidad del nombre y apellido del autor
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (existingAuthors.Any(a => a.Nombre == author.Nombre && a.Apellido == author.Apellido))
            {
                _logger.LogError("Error, el nombre del autor ya existe en la base de datos.");
                throw new InvalidOperationException("El nombre del autor ya existe.");
            }

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAuthorAsync(Autor author)
        {
            _logger.LogInformation("Actualizando autor con Id {AutorId} en la base de datos.", author.Id);

            // Se agregan los campos de auditoría por defecto
            author.FechaModificacion = DateTime.Now;
            author.UsuarioModificacion = "System";

            // Se verifica la unicidad del nombre y apellido del autor
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (existingAuthors.Any(a => a.Nombre == author.Nombre && a.Apellido == author.Apellido && a.Id != author.Id))
            {
                _logger.LogError("Error, el nombre del autor ya existe en la base de datos.");
                throw new InvalidOperationException("El nombre del autor ya existe.");
            }

            // Desacoplar la entidad existente
            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(author.Id);
            if (existingAuthor == null)
            {
                throw new InvalidOperationException("El autor no existe.");
            }

            // Se actualizan los valores del autor existente
            existingAuthor.Nombre = author.Nombre;
            existingAuthor.Apellido = author.Apellido;
            existingAuthor.IdTipoDocto = author.IdTipoDocto;
            existingAuthor.NumDocto = author.NumDocto;
            existingAuthor.FechaNacimiento = author.FechaNacimiento;
            existingAuthor.Bibliografia = author.Bibliografia;
            existingAuthor.FechaModificacion = author.FechaModificacion;
            existingAuthor.UsuarioModificacion = author.UsuarioModificacion;

            _unitOfWork.Authors.Update(existingAuthor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            _logger.LogInformation("Eliminando autor con Id {AutorId} de la base de datos.", id);

            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                throw new InvalidOperationException("El autor no existe.");
            }

            _unitOfWork.Authors.Delete(existingAuthor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Guardando cambios en la base de datos.");
            return await _unitOfWork.CompleteAsync();
        }     
    }
}
