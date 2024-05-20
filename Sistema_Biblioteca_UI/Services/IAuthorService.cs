using Sistema_Biblioteca_Shared;
using Sistema_Biblioteca_UI.Pages;

namespace Sistema_Biblioteca.Services
{
    public interface IAuthorService
    {
        Task<(IEnumerable<AutorDTO>? data, string? error)> GetAllAuthors();
        Task<(AutorDTO? data, string? error)> GetAuthorById(int id);
        Task<string> AddAuthor(AutorDTO author);
        Task<string> UpdateAuthor(AutorDTO author);
        Task<string> DeleteAuthor(int id);
    }
}
