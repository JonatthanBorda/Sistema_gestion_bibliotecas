using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;

namespace API_Biblioteca.BLL.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookService> _logger;

        public AuthorService(IUnitOfWork unitOfWork, ILogger<BookService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Autor>> GetAllAuthorsAsync()
        {
            _logger.LogInformation("Obteniendo todos los autores de la base de datos.");
            return await _unitOfWork.Authors.GetAllAsync();
        }
        public async Task<Autor> GetAuthorByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo autor con Id {AutorId} de la base de datos.", id);
            return await _unitOfWork.Authors.GetByIdAsync(id);
        }
            

        public async Task AddAuthorAsync(Autor author)
        {
            _logger.LogInformation("Agregando un nuevo autor a la base de datos.");

            //Se verifica la unicidad del nombre del autor:
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (existingAuthors.Any(a => a.Nombre == author.Nombre))
            {
                _logger.LogError("Error, el nombre del autor ya existe en la base de datos.");
                throw new InvalidOperationException("El nombre del autor ya existe.");
            }
            await _unitOfWork.Authors.AddAsync(author);
        }

        public async Task UpdateAuthor(Autor author)
        {
            _logger.LogInformation("Actualizando autor con Id {AutorId} en la base de datos.", author.Id);
            //Se verifica la unicidad del nombre del autor:
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (existingAuthors.Any(a => a.Nombre == author.Nombre && a.Id != author.Id))
            {
                _logger.LogError("Error, el nombre del autor ya existe en la base de datos.");
                throw new InvalidOperationException("El nombre del autor ya existe.");
            }
            _unitOfWork.Authors.Update(author);
        }

        public void DeleteAuthor(Autor author)
        {
            _logger.LogInformation("Eliminado autor con Id {AutorId} de la base de datos.", author.Id);
            _unitOfWork.Authors.Delete(author);
        }
            
        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Guardando cambios en la base de datos.");
            return await _unitOfWork.CompleteAsync();
        }     
    }
}
