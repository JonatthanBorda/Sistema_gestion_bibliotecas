using Sistema_Biblioteca_Shared;
using Sistema_Biblioteca_UI.Pages;

namespace Sistema_Biblioteca.Services
{
    public interface IBookService
    {
        Task<(IEnumerable<LibroDTO>? data, string? error)> GetAllBooks();
        Task<(LibroDTO? data, string? error)> GetBookById(int id);
        Task<string> AddBook(LibroDTO author);
        Task<string> UpdateBook(LibroDTO author);
        Task<string> DeleteBook(int id);
    }
}
