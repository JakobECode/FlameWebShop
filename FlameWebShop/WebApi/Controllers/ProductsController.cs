using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
       // private readonly ProductRepository _productRepo;
        private readonly ProductService _productService;

        // Konstruktor för ProductsController. Initialiserar fälten med de instanser som förmedlas vid instansiering.
        // Denna beroendeinjektion möjliggör att byta ut och testa olika delar av systemet.
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [Route("id/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var product = await _productService.GetAsync(p => p.Id == id);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound($"Produkt med ID {id} hittades inte.");
                }
            }
            catch (Exception ex)
            {
                // Logga undantaget och returnera ett serverfel
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, "Ett internt serverfel har inträffat.");
            }
        }

        // HTTP GET metod för att hämta alla produkter, [Route("all")] definierar sub-routen för denna åtgärd.
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Anropar ProductService för att hämta alla produkter och returnerar dem.
            return Ok(await _productService.GetAllAsync());
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
        public async Task<IActionResult> AddAsync(Product product)
        {
            // Anropar ProductService för att skapa en ny produkt och returnerar den skapade produkten.
            return Ok(await _productService.AddAsync(product));
        }

        // HTTP DELETE metod för att ta bort en produkt baserat på dess ID. [Route("delete/{id}")] definierar sub-routen för denna åtgärd.
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult>DeleteAsync(int id)
        {
            // Anropar ProductService för att ta bort en produkt baserat på ID och returnerar resultatet.
            var success = await _productService.DeleteAsync(id);
            if (success)
                return Ok(); 
            else
                return NotFound(); 
        }

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, Product product)
        {
            if (product == null)
            {
                return BadRequest("Uppdaterad produktinformation saknas.");
            }

            try
            {
                //var existingProduct = await _productService.GetAsync(x => x.Id == product.Id);
                //if (existingProduct == null)
                //{
                //    return NotFound($"Produkt med ID {id} hittades inte.");
                //}
                var success = await _productService.UpdateAsync(product);
                if (success)
                {
                    return Ok($"Produkt med ID {product.Id} har uppdaterats.");
                }
                else
                {
                    return StatusCode(500, "Ett fel inträffade vid uppdatering av produkten.");
                }
            }
            catch (Exception ex)
            {
                // Loggar undantaget
                // Log.Error(ex, "Ett fel inträffade när produkten skulle uppdateras.");
                return StatusCode(500, "Ett internt serverfel inträffade.");
            }
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
