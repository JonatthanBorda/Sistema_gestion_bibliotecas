using API_Biblioteca.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Biblioteca.BLL.Services
{
    public class LoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(string token, string role, string error)> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                {
                    return (null, null, "Credenciales inválidas.");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault(); // Suponiendo que el usuario tiene un único rol

                var token = GenerateJwtToken(user, role);
                return (token, role, null);
            }
            catch (Exception ex)
            {
                return (null, null, $"Se ha presentado una excepción: {ex.Message}");
            }
        }


        public async Task<string> RegisterAsync(string username, string password, string role)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(username);
                if (existingUser != null)
                {
                    return "El nombre de usuario ya existe.";
                }

                var user = new ApplicationUser
                {
                    UserName = username,
                    Role = role
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    // Recopilar errores detallados del resultado
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return errors;
                }

                var roleResult = await _userManager.AddToRoleAsync(user, role);

                if (!roleResult.Succeeded)
                {
                    // Recopilar errores detallados del resultado de la asignación de rol
                    var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return roleErrors;
                }

                return null;
            }
            catch (Exception ex)
            {
                return $"Se ha presentado una excepción: {ex.Message}";
            }
        }

        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role) // Incluir el rol en los claims
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
