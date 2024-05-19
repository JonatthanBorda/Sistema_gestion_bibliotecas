using API_Biblioteca.DAL.Models;

namespace API_Biblioteca.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Libro> Books { get; }
        IRepository<Autor> Authors { get; }
        Task<int> CompleteAsync();
    }
}
