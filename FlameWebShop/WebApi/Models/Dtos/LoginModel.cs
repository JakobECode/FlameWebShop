using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Dtos
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Password is too weak. Include at least one number and one special sign")]
        public string Password { get; set; } = null!;

        public static implicit operator IdentityUser(LoginModel model)
        {
            return new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email
            };
        }
    }
    /* 
      Denna klass användas för hantering av inloggningsuppgifter.
      Attributen[Required] och [RegularExpression] används för att
      säkerställa att e-postadressen och lösenordet uppfyller vissa kriterier innan de behandlas,
      vilket är viktigt för både säkerhet och dataintegritet.
      Den implicita konverteringen till IdentityUser är användbar för att integrera med
      ASP.NET Core Identity, ett system för autentisering och användarhantering.
    */
}
