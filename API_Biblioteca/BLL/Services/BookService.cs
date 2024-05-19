using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.BLL.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Libro>> GetAllBooksAsync() => await _unitOfWork.Books.GetAllAsync();
        public async Task<Libro> GetBookByIdAsync(int id) => await _unitOfWork.Books.GetByIdAsync(id);
        public async Task AddBookAsync(Libro book) => await _unitOfWork.Books.AddAsync(book);
        public void UpdateBook(Libro book) => _unitOfWork.Books.Update(book);
        public void DeleteBook(Libro book) => _unitOfWork.Books.Delete(book);
        public async Task<int> CompleteAsync() => await _unitOfWork.CompleteAsync();
    }
}
