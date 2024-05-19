using API_Biblioteca.DAL.Models;

namespace API_Biblioteca.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbLibrarySystemContext _context;
        public IRepository<Libro> Books { get; private set; }
        public IRepository<Autor> Authors { get; private set; }

        public UnitOfWork(DbLibrarySystemContext context)
        {
            _context = context;
            Books = new BookRepository(context);
            Authors = new AuthorRepository(context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
