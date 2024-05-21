using Sistema_Biblioteca_Shared;
using System.Net.Http.Json;

namespace Sistema_Biblioteca.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(IEnumerable<AutorDTO>? data, string? error)> GetAllAuthors()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Autores");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<IEnumerable<AutorDTO>>();
                    return (data, null);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (null, $"Se ha presentado un error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (null, $"Se ha presentado una excepción: {ex.Message}");
            }
        }

        public async Task<(AutorDTO? data, string? error)> GetAuthorById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Autores/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<AutorDTO>();
                    return (data, null);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (null, $"Se ha presentado un error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (null, $"Se ha presentado una excepción: {ex.Message}");
            }
        }

        public async Task<string> AddAuthor(AutorDTO author)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Autores", author);
                if (response.IsSuccessStatusCode)
                {
                    return null!;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return $"Acceso denegado. No tienes permisos para realizar esta acción.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Se ha presentado un error: {errorContent}"; //Se retorna error de la API
                }
            }
            catch (Exception ex)
            {
                return $"Se ha presentado una excepción: {ex.Message}"; //Se retorna mensaje de excepción
            }
        }

        public async Task<string> UpdateAuthor(AutorDTO author)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Autores/{author.Id}", author);
                if (response.IsSuccessStatusCode)
                {
                    return null!;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return $"Acceso denegado. No tienes permisos para realizar esta acción.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Se ha presentado un error: {errorContent}"; //Se retorna error de la API
                }
            }
            catch (Exception ex)
            {
                return $"Se ha presentado una excepción: {ex.Message}" ; //Se retorna mensaje de excepción
            }
        }

        public async Task<string> DeleteAuthor(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Autores/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return null!;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return $"Acceso denegado. No tienes permisos para realizar esta acción.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Se ha presentado un error: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                return $"Se ha presentado una excepción: {ex.Message}";
            }
        }
    }
}
