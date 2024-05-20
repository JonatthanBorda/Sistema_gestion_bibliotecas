using API_Biblioteca.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.DAL.Repositories
{
    public class AuthorRepository : IRepository<Autor>
    {
        private readonly DbLibrarySystemContext _context;

        public AuthorRepository(DbLibrarySystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>> GetAllAsync() => await _context.Autors.Include(l => l.Libros).ToListAsync();
        public async Task<Autor> GetByIdAsync(int id) => await _context.Autors.Include(l => l.Libros).FirstOrDefaultAsync(a => a.Id == id);
        public async Task AddAsync(Autor entity) => await _context.Autors.AddAsync(entity);
        public void Update(Autor entity) => _context.Autors.Update(entity);
        public void Delete(Autor entity) => _context.Autors.Remove(entity);
    }
}
