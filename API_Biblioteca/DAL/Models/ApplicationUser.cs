using Microsoft.AspNetCore.Identity;

namespace API_Biblioteca.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = null!;
    }
}
