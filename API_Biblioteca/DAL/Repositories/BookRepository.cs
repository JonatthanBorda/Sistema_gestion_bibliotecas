using API_Biblioteca.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Sistema_Biblioteca_Shared;

namespace API_Biblioteca.DAL.Repositories
{
    public class BookRepository : IRepository<Libro>
    {
        private readonly DbLibrarySystemContext _context;

        public BookRepository(DbLibrarySystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Libro>> GetAllAsync() => await _context.Libros.Include(l => l.IdAutorNavigation).ToListAsync();
        public async Task<Libro> GetByIdAsync(int id) => await _context.Libros.Include(l => l.IdAutorNavigation).FirstOrDefaultAsync(l => l.Id == id);
        public async Task AddAsync(Libro entity) => await _context.Libros.AddAsync(entity);
        public void Update(Libro entity) => _context.Libros.Update(entity);
        public void Delete(Libro entity) => _context.Libros.Remove(entity);
    }
}
