using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Services;
using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // Privata fält för att lagra referenser till AuthenticationService, UserManager och SignInManager.
        // Dessa används för att hantera användarautentisering och registrering.
        private readonly AuthenticationService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // Konstruktor för AuthenticationController.
        // Initialiserar fälten med de instanser som förmedlas vid instansiering.
        // Denna beroendeinjektion underlättar testning och underhåll.
        public AuthenticationController(AuthenticationService authService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // En HTTP POST metod för att registrera en ny användare.
        // Använder modellen RegisterModel för att ta emot användarregistreringsdata.
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)  // Kontrollerar att modellen är giltig enligt dess definition.
            {
                if (await _authService.RegisterAsync(model))
                    return Created("", null!); // Returnerar en HTTP 201 (Created) status om registreringen lyckas.
            }

            return BadRequest();  // Returnerar en HTTP 400 (BadRequest) status om registreringen misslyckas.
        }

        // En HTTP POST metod för att logga in en användare.
        // Använder modellen LoginModel för att ta emot användarinloggningsdata.
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)  // Kontrollerar att modellen är giltig enligt dess definition.
                {
                    var token = await _authService.LoginAsync(model); // Försöker logga in användaren.
                    if (!string.IsNullOrEmpty(token)) // Returnerar en JWT-token om inloggningen lyckas.
                        return Ok(token);

                    ModelState.AddModelError("", "Incorrect email or password");
                    return Unauthorized(model); // Returnerar en HTTP 401 (Unauthorized) status om inloggningen misslyckas.
                }

                return BadRequest(model); // Returnerar en HTTP 400 (BadRequest) status om modellen inte är giltig.
            }
            catch { return Problem(); } // Returnerar en HTTP 500 (Internal Server Error) vid undantag.
        }
    }
}
/*
 AuthenticationController-klassen hanterar användarregistrering och inloggning i en ASP.NET Core applikation.
 Den använder AuthenticationService för att utföra dessa operationer samt UserManager och SignInManager för 
 att hantera användarrelaterade operationer. 
 Metoden Register tar emot registreringsdata och skapar en ny användare om datan är giltig.
 Metoden Login autentiserar användaren och genererar en JWT-token vid framgångsrik inloggning.
 Felhantering inkluderar att returnera lämpliga HTTP-statuskoder baserat på olika fall, 
 såsom ogiltig data eller autentiseringsfel.
*/