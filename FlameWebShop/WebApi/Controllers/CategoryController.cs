using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Repositories;

namespace WebApi.Controllers
{
    // Använder attributet [Route] för att definiera grundläggande routen för alla åtgärder i denna kontroller.
    // [ApiController] attributet indikerar att denna klass är en ASP.NET Core webb-API-kontroller.

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Ett privat fält för att lagra en referens till CategoryRepository.
        // Det används för att hantera databasoperationer relaterade till kategorier.
        private readonly CategoryRepository _categoryRepo;

        // Konstruktor för CategoriesController.
        // Initialiserar _categoryRepo-fältet med den instans som förmedlas vid instansiering.
        // Denna beroendeinjektion underlättar testning och underhåll.
        public CategoriesController(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // En HTTP GET metod för att hämta alla kategorier. [Route("all")] definierar sub-routen för denna åtgärd.
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Anropar CategoryRepository för att hämta alla kategorier och returnerar dem.
            return Ok(await _categoryRepo.GetAllAsync());
        }
    }
}
/*
CategoriesController-klassen är en webb-API-kontroller som hanterar HTTP-förfrågningar relaterade till kategorier.
Den använder en instans av CategoryRepository för att utföra databasoperationer.
Metoden GetAllAsync är en HTTP GET-åtgärd som hämtar alla kategorier från databasen asynkront och returnerar dem.
Användningen av async och await möjliggör asynkron hantering av förfrågningar, vilket förbättrar applikationens
prestanda och skalbarhet.
*/
