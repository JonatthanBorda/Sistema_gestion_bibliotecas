using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Sistema_Biblioteca_Shared;
using Sistema_Biblioteca_UI.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static Sistema_Biblioteca_UI.Pages.Login;
using static Sistema_Biblioteca_UI.Pages.Register;

namespace Sistema_Biblioteca.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public LoginService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Login/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    await _localStorage.SetItemAsync("authToken", loginResponse?.Token);
                    ((CustomLoginStateProvider)_authStateProvider).NotifyUserAuthentication(loginResponse.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponse.Token);
                    return string.Empty;
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                return $"Intento de inicio de sesión no válido: {errorResponse}";
            }
            catch (Exception ex)
            {
                return $"Se ha presentado una excepción: {ex.Message}";
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomLoginStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task LogoutAndRedirect(NavigationManager navigationManager)
        {
            await Logout();
            navigationManager.NavigateTo("/");
        }

        public async Task<string> Register(RegisterModel registerModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Login/register", registerModel);

                if (response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                return $"Error al registrarse: {errorResponse}";
            }
            catch (Exception ex)
            {
                return $"Se ha presentado una excepción: {ex.Message}";
            }
        }


        public class LoginResponse
        {
            public string? Token { get; set; }
        }
    }
}
