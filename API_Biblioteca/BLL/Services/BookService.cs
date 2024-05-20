using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Biblioteca_Shared;

namespace API_Biblioteca.BLL.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookService> _logger;

        public BookService(IUnitOfWork unitOfWork, ILogger<BookService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<LibroDTO>> GetAllBooksAsync()
        {
            _logger.LogInformation("Obteniendo todos los libros de la base de datos.");
            var books = await _unitOfWork.Books.GetAllAsync();
            return books.Select(book => new LibroDTO
            {
                Id = book.Id,
                Titulo = book.Titulo,
                NumPaginas = book.NumPaginas,
                FechaPublicacion = book.FechaPublicacion,
                Disponible = book.Disponible,
                IdAutor = book.IdAutor,
                Autor = new AutorDTO
                {
                    Id = book.IdAutorNavigation.Id,
                    Nombre = book.IdAutorNavigation.Nombre,
                    Apellido = book.IdAutorNavigation.Apellido,
                    IdTipoDocto = book.IdAutorNavigation.IdTipoDocto,
                    FechaNacimiento = book.IdAutorNavigation.FechaNacimiento
                }
            }).ToList();
        }

        public async Task<LibroDTO> GetBookByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo libro con Id {LibroId} de la base de datos.", id);
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                return null;

            return new LibroDTO
            {
                Id = book.Id,
                Titulo = book.Titulo,
                NumPaginas = book.NumPaginas,
                FechaPublicacion = book.FechaPublicacion,
                Disponible = book.Disponible,
                IdAutor = book.IdAutor,
                Autor = new AutorDTO
                {
                    Id = book.IdAutorNavigation.Id,
                    Nombre = book.IdAutorNavigation.Nombre,
                    Apellido = book.IdAutorNavigation.Apellido,
                    IdTipoDocto = book.IdAutorNavigation.IdTipoDocto,
                    FechaNacimiento = book.IdAutorNavigation.FechaNacimiento
                }
            };
        }

        public async Task AddBookAsync(LibroDTO bookDto)
        {
            _logger.LogInformation("Agregando un nuevo libro a la base de datos.");

            var book = new Libro
            {
                Titulo = bookDto.Titulo,
                NumPaginas = bookDto.NumPaginas,
                FechaPublicacion = bookDto.FechaPublicacion,
                Disponible = bookDto.Disponible,
                IdAutor = bookDto.IdAutor,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = "System"
            };

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateBookAsync(LibroDTO bookDto)
        {
            _logger.LogInformation("Actualizando libro con Id {LibroId} en la base de datos.", bookDto.Id);

            var existingBook = await _unitOfWork.Books.GetByIdAsync(bookDto.Id);
            if (existingBook == null)
            {
                _logger.LogError("El libro con Id {LibroId} no existe en la base de datos.", bookDto.Id);
                throw new InvalidOperationException("El libro no existe.");
            }

            existingBook.Titulo = bookDto.Titulo;
            existingBook.NumPaginas = bookDto.NumPaginas;
            existingBook.FechaPublicacion = bookDto.FechaPublicacion;
            existingBook.Disponible = bookDto.Disponible;
            existingBook.IdAutor = bookDto.IdAutor;
            existingBook.FechaModificacion = DateTime.Now;
            existingBook.UsuarioModificacion = "System";

            _unitOfWork.Books.Update(existingBook);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            _logger.LogInformation("Eliminando libro con Id {LibroId} de la base de datos.", id);

            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
            {
                _logger.LogError("El libro con Id {LibroId} no existe en la base de datos.", id);
                throw new InvalidOperationException("El libro no existe.");
            }

            _unitOfWork.Books.Delete(existingBook);
            await _unitOfWork.CompleteAsync();
        }
        
        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Guardando cambios en la base de datos.");
            return await _unitOfWork.CompleteAsync();
        }
    }
}
