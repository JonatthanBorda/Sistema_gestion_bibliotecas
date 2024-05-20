using API_Biblioteca.DAL.Models;
using API_Biblioteca.DAL.Repositories;

namespace API_Biblioteca.BLL.Services
{
    public class DocTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DocTypeService> _logger;

        public DocTypeService(IUnitOfWork unitOfWork, ILogger<DocTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<TipoDocto>> GetAllDocsTypeAsync()
        {
            _logger.LogInformation("Obteniendo todos los tipos de documento de la base de datos.");
            return await _unitOfWork.DocsType.GetAllAsync();
        }

        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Guardando cambios en la base de datos.");
            return await _unitOfWork.CompleteAsync();
        }     
    }
}
