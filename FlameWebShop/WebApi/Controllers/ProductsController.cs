using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Repositories;
using WebApi.Helpers.Services;
using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    // Använder attributet [Route] för att definiera grundläggande routen för alla åtgärder i denna kontroller.
    // [ApiController] attributet indikerar att denna klass är en ASP.NET Core webb-API-kontroller.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Privata fält för att lagra referenser till ProductRepository och ProductService.
        // Dessa används för att hantera databasoperationer och affärslogik för produkter.
        private readonly ProductRepository _productRepo;
        private readonly ProductService _productService;

        // Konstruktor för ProductsController. Initialiserar fälten med de instanser som förmedlas vid instansiering.
        // Denna beroendeinjektion möjliggör att byta ut och testa olika delar av systemet.
        public ProductsController(ProductRepository productRepo, ProductService productService)
        {
            _productRepo = productRepo;
            _productService = productService;
        }

        // HTTP GET metod för att hämta alla produkter, [Route("all")] definierar sub-routen för denna åtgärd.
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Anropar ProductService för att hämta alla produkter och returnerar dem.
            return Ok(await _productRepo.GetAllAsync());
        }

        // HTTP GET metod för att hämta en produkt baserat på dess ID. [Route("id")] definierar sub-routen för denna åtgärd.
        [Route("id")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // Anropar ProductService för att hämta en produkt baserat på ID och returnerar den.
            return Ok(await _productService.GetAsync(x => x.Id == id));
        }

        // En HTTP GET metod för att hämta produkter baserat på deras tagg. [Route("tag")] definierar sub-routen för denna åtgärd.
        [Route("tag")]
        [HttpGet]
        public async Task<IActionResult> GetByTagAsync(string tag)
        {
            // Anropar ProductService för att hämta produkter baserat på tagg och returnerar dem.
            return Ok(await _productService.GetAllAsync(x => x.Tag == tag));
        }

        // En HTTP POST metod för att lägga till en ny produkt. [Route("add")] definierar sub-routen för denna åtgärd.
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            // Anropar ProductService för att skapa en ny produkt och returnerar den skapade produkten.
            return Ok(await _productService.CreateAsync(product));
        }
    }
}
/*
   ProductsController-klassen är en webb-API-kontroller i en ASP.NET Core applikation.
   Den hanterar HTTP-förfrågningar relaterade till produkter. 
   Klassen använder ProductService och ProductRepository för att hantera affärslogik och databasåtkomst.
   Varje metod i kontrollern motsvarar en särskild HTTP-rutt och åtgärd, såsom att hämta alla produkter, 
   hämta en produkt baserat på ID eller tagg, och lägga till en ny produkt.
   Användningen av async och await möjliggör asynkron hantering av förfrågningar, 
   vilket förbättrar applikationens prestanda och skalbarhet.
*/
