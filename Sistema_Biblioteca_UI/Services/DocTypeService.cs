using Sistema_Biblioteca_Shared;
using System.Net.Http.Json;

namespace Sistema_Biblioteca.Services
{
    public class DocTypeService : IDocTypeService
    {
        private readonly HttpClient _httpClient;

        public DocTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TipoDoctoDTO>> GetAllTypes()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TipoDoctoDTO>>("api/TiposDocto");
        }
    }
}
