using Microsoft.AspNetCore.Components;
using Sistema_Biblioteca_Shared;
using Sistema_Biblioteca_UI.Pages;
using static Sistema_Biblioteca_UI.Pages.Login;
using static Sistema_Biblioteca_UI.Pages.Register;

namespace Sistema_Biblioteca.Services
{
    public interface ILoginService
    {
        Task<string> Login(LoginModel loginModel);
        Task Logout();
        Task LogoutAndRedirect(NavigationManager navigationManager);
        Task<string> Register(RegisterModel registerModel);
    }
}
