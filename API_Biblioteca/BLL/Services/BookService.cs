using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Libro>> GetAllBooksAsync()
        {
            _logger.LogInformation("Obteniendo todos los libros de la base de datos.");
            return await _unitOfWork.Books.GetAllAsync();
        }
            
        public async Task<Libro> GetBookByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo libro con Id {LibroId} de la base de datos.", id);
            return await _unitOfWork.Books.GetByIdAsync(id);
        }
            
        public async Task AddBookAsync(Libro book)
        {
            _logger.LogInformation("Agregando un nuevo libro a la base de datos.");
            await _unitOfWork.Books.AddAsync(book);
        }
            
        public void UpdateBook(Libro book)
        {
            _logger.LogInformation("Actualizando libro con Id {LibroId} en la base de datos.", book.Id);
            _unitOfWork.Books.Update(book);
        }
            
        public void DeleteBook(Libro book)
        {
            _logger.LogInformation("Eliminado libro con Id {LibroId} de la base de datos.", book.Id);
            _unitOfWork.Books.Delete(book);
        }

        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Guardando cambios en la base de datos.");
            return await _unitOfWork.CompleteAsync();
        }
    }
}
