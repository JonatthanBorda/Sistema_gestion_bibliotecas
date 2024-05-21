using API_Biblioteca.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(LoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var (token, role, error) = await _loginService.LoginAsync(model.Username, model.Password);
                if (token != null)
                {
                    _logger.LogInformation("Usuario logueado. Se ha generado el token correctamente");
                    return Ok(new { Token = token, Role = role });
                }
                return Unauthorized(new { Error = error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controlador - Error durante el inicio de sesión. {ex.Message}");
                return StatusCode(500, new { message = $"Ocurrió un error inesperado. {ex.Message}" });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var error = await _loginService.RegisterAsync(model.Username, model.Password, model.Role);
                if (string.IsNullOrEmpty(error))
                {
                    return Ok(new { Message = "Usuario registrado correctamente." });
                }
                return BadRequest(new { Error = error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controlador - Error durante el registro. {ex.Message}");
                return StatusCode(500, new { message = $"Ocurrió un error inesperado. {ex.Message}" });
            }
        }

    }

    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class RegisterModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
