using API_Biblioteca.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.DAL.Repositories
{
    public class DocTypeRepository : IRepository<TipoDocto>
    {
        private readonly DbLibrarySystemContext _context;

        public DocTypeRepository(DbLibrarySystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoDocto>> GetAllAsync() => await _context.TipoDoctos.ToListAsync();
        public async Task<TipoDocto> GetByIdAsync(int id) => await _context.TipoDoctos.FindAsync(id);
        public async Task AddAsync(TipoDocto entity) => await _context.TipoDoctos.AddAsync(entity);
        public void Update(TipoDocto entity) => _context.TipoDoctos.Update(entity);
        public void Delete(TipoDocto entity) => _context.TipoDoctos.Remove(entity);
    }
}
