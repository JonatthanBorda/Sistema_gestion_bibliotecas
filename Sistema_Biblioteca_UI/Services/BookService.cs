using Sistema_Biblioteca_Shared;
using System.Net.Http.Json;

namespace Sistema_Biblioteca.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(IEnumerable<LibroDTO>? data, string? error)> GetAllBooks()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Libros");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<IEnumerable<LibroDTO>>();
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

        public async Task<(LibroDTO? data, string? error)> GetBookById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Libros/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<LibroDTO>();
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

        public async Task<string> AddBook(LibroDTO author)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Libros", author);
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

        public async Task<string> UpdateBook(LibroDTO author)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Libros/{author.Id}", author);
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

        public async Task<string> DeleteBook(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Libros/{id}");
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
