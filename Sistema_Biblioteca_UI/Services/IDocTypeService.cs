using Sistema_Biblioteca_Shared;

namespace Sistema_Biblioteca.Services
{
    public interface IDocTypeService
    {
        Task<IEnumerable<TipoDoctoDTO>> GetAllTypes();
    }
}
