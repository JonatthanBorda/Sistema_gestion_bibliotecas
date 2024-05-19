using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;

namespace API_Biblioteca.BLL.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Autor>> GetAllAuthorsAsync() => await _unitOfWork.Authors.GetAllAsync();
        public async Task<Autor> GetAuthorByIdAsync(int id) => await _unitOfWork.Authors.GetByIdAsync(id);

        public async Task AddAuthorAsync(Autor author)
        {
            //Se verifica la unicidad del nombre del autor:
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (existingAuthors.Any(a => a.Nombre == author.Nombre))
            {
                throw new InvalidOperationException("El nombre del autor ya existe.");
            }
            await _unitOfWork.Authors.AddAsync(author);
        }

        public async Task UpdateAuthor(Autor author)
        {
            //Se verifica la unicidad del nombre del autor:
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (existingAuthors.Any(a => a.Nombre == author.Nombre && a.Id != author.Id))
            {
                throw new InvalidOperationException("El nombre del autor ya existe.");
            }
            _unitOfWork.Authors.Update(author);
        }

        public void DeleteAuthor(Autor author) => _unitOfWork.Authors.Delete(author);
        public async Task<int> CompleteAsync() => await _unitOfWork.CompleteAsync();
    }
}
