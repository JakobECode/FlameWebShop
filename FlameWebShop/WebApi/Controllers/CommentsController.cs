using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    // Använder attributet [Route] för att definiera grundläggande routen för alla åtgärder i denna kontroller.
    // [ApiController] attributet indikerar att denna klass är en ASP.NET Core webb-API-kontroller.

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        // Ett privat fält för att lagra en referens till CommentRepository.
        // Det används för att hantera databasoperationer relaterade till kommentarer.
        private readonly CommentRepository _commentRepository;

        // Konstruktor för CommentsController Initialiserar _commentRepository-fältet med den instans som
        // förmedlas vid instansiering. Denna beroendeinjektion underlättar testning och underhåll.
        public CommentsController(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // En HTTP POST metod för att lägga till en ny kommentar. [Route("add")] definierar sub-routen för denna åtgärd.
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(Comment comment)
        {
            // Anropar CommentRepository för att lägga till den nya kommentaren i databasen och returnerar resultatet.
            return Ok(await _commentRepository.AddAsync(comment));
        }

        // En HTTP GET metod för att hämta alla kommentarer. [Route("all")] definierar sub-routen för denna åtgärd.
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Anropar CommentRepository för att hämta alla kommentarer och returnerar dem.
            return Ok(await _commentRepository.GetAllAsync());
        }
    }

   
}
/*
 CommentsController-klassen är en webb-API-kontroller som hanterar HTTP-förfrågningar relaterade till kommentarer. 
 Den använder en instans av CommentRepository för att utföra databasoperationer.
 Varje metod i kontrollern motsvarar en särskild HTTP-rutt och åtgärd, såsom att lägga till en ny 
 kommentar eller hämta alla kommentarer.
 Användningen av async och await möjliggör asynkron hantering av förfrågningar, 
 vilket förbättrar applikationens prestanda och skalbarhet.
*/