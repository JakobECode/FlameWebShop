using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class RegisterModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Password is too weak. Include at least one number and one special sign")]
        public string Password { get; set; } = null!;

        public static implicit operator IdentityUser(RegisterModel model)
        {
            return new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email
            };
        }

        public static implicit operator UserProfileEntity(RegisterModel model)
        {
            return new UserProfileEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        /*
          Denna klass användas för att hantera registreringsdata för användare i en
          .NET-applikationen. Den inkluderar användarens förnamn, efternamn, e-postadress och lösenord.
          Attributen [Required] och [RegularExpression] används för att säkerställa att e-postadressen
          och lösenordet uppfyller vissa kriterier.
          De implicita konverteringsmetoderna till IdentityUser och UserProfileEntity förenklar processen
          att skapa dessa entiteter baserade på registreringsdata,
          vilket är användbart i sammanhang där ASP.NET Core Identity och användarprofiler hanteras.
        */

    }
}
