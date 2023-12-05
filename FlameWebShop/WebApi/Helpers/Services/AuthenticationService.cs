using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Services
{
    // Denna klass ansvarar för att hantera användarautentisering och registrering i en ASP.NET Core applikation.
    public class AuthenticationService
    {
        // Privata fält för att lagra referenser till UserManager, SignInManager, RoleManager och UserProfileRepository.
        // Dessa används för att hantera användarrelaterade operationer som skapande, autentisering och rollhantering.

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserProfileRepository _profileRepo;

        // Konstruktor för AuthenticationService. 
        // Initialiserar fälten med de instanser som förmedlas vid instansiering.
        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserProfileRepository profileRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _profileRepo = profileRepo;
        }
        // Asynkron metod för att registrera en ny användare.
        // Skapar en ny IdentityUser, tilldelar en roll, och sparar användarens profilinformation.
        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            IdentityUser user = model;  // Konverterar RegisterModel till IdentityUser.
            var isFirstUser = !_userManager.Users.Any(); // Kontrollerar om detta är den första användaren som registrerar sig.
            var result = await _userManager.CreateAsync(user, model.Password); // Skapar användaren i systemet.
            if (result.Succeeded)
            {
                var role = isFirstUser ? "Admin" : "Produktansvarig"; // Tilldelar en roll baserat på om användaren är den första.
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role)); // Skapar rollen om den inte redan finns.

                await _userManager.AddToRoleAsync(user, role); // Tilldelar rollen till användaren.

                var createdUser = await _userManager.FindByEmailAsync(model.Email!); // Hittar den nyskapade användaren.

                UserProfileEntity profile = model; // Konverterar RegisterModel till UserProfileEntity.
                profile.UserId = createdUser!.Id; // Anger UserId för profilen.
                await _profileRepo.AddAsync(profile); // Sparar profilen i databasen.

                return true;
            }

            return false;
        }
        // Asynkron metod för att logga in en användare. Autentiserar användaren och genererar en JWT om autentiseringen lyckas.
        public async Task<string> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);  // Söker efter användaren baserat på e-postadressen.
            if (user != null)
            {
                var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false); // Kontrollerar lösenordet.
                if (passwordCheck.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user); // Hämtar användarens roll/roller.
                    var claims = new ClaimsIdentity(new Claim[]
                        {
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Role, role.First()),
                    new Claim(ClaimTypes.Name, user.Email!)
                        });

                    // Genererar och returnerar en JWT med användaranspråk (claims) och en utgångstid på 1 dag.
                    return TokenGenerator.Generate(claims, DateTime.Now.AddDays(1));
                }
            }

            return string.Empty; // Returnerar en tom sträng om autentiseringen misslyckas.
        }
    }
}
/*
  AuthenticationService-klassen tillhandahåller funktionalitet för att registrera och autentisera användare.
  Den använder ASP.NET Core Identity-ramverkets klasser som UserManager, SignInManager, och RoleManager 
  för att hantera användarrelaterade operationer. 
  För registrering skapar den en ny användare, tilldelar en roll 
  (Admin eller Produktansvarig beroende på användarens status), och sparar användarprofilen.
  För inloggning autentiserar den användaren, genererar användaranspråk (claims) och skapar en JWT för autentiser.
*/