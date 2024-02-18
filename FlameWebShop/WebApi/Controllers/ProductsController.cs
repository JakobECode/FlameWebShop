using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using WebApi.Helpers.Interfaces;
using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
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
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Ett internt serverfel har inträffat.");
                }
                return StatusCode(500, "Ett internt serverfel har inträffat.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, "Ett internt serverfel har inträffat.");
            }
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                if (products != null) 
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound("Inga produkter hittades.");
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Ett fel uppstod vid hämtning av alla produkter.");
                }
                return StatusCode(500, "Ett internt serverfel har inträffat vid hämtning av alla produkter.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, "Ett internt serverfel har inträffat vid hämtning av alla produkter.");
            }
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(Product product)
        {
            try
            {
                var createdProduct = await _productService.AddAsync(product);
                return Ok(createdProduct);
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Console.WriteLine("HTTP Statuskod: " + (int)response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Ett fel uppstod vid tillägg av en ny produkt.");
                }
                return StatusCode(500, "Ett internt serverfel har inträffat vid tillägg av en ny produkt.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, "Ett internt serverfel har inträffat vid tillägg av en ny produkt.");
            }
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var success = await _productService.DeleteAsync(id);
                if (success)
                    return Ok();
                else
                    return NotFound($"Produkt med ID {id} hittades inte för borttagning.");
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Console.WriteLine("HTTP Statuskod: " + (int)response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Ett fel uppstod vid försök att ta bort produkt.");
                }
                return StatusCode(500, "Ett internt serverfel har inträffat vid försök att ta bort produkt.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, "Ett internt serverfel har inträffat vid försök att ta bort produkt.");
            }
        }

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, Product product)
        {
            if (product == null)
            {
                return BadRequest("Uppdaterad produktinformation saknas.");
            }
            else if (product.Id != id)
            {
                return BadRequest("Produkt-ID stämmer inte överens med ID i förfrågan.");
            }

            try
            {
                var success = await _productService.UpdateAsync(product);
                if (success)
                {
                    return Ok($"Produkt med ID {product.Id} har uppdaterats.");
                }
                else
                {
                    return NotFound($"Produkt med ID {product.Id} hittades inte.");
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Console.WriteLine("HTTP Statuskod: " + (int)response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Ett nätverksfel uppstod vid uppdatering av produkten.");
                }
                return StatusCode(500, "Ett nätverksfel uppstod vid uppdatering av produkten.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, "Ett internt serverfel inträffade vid uppdatering av produkten.");
            }
        }

        [Route("insert")]
        [HttpPost]
        public async Task<IActionResult> InsertAsync(Product product)
        {
            if (product == null)
            {
                return BadRequest("Produktinformation saknas.");
            }

            try
            {
                var result = await _productService.InsertAsync(product);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, "Kunde inte lägga till produkten.");
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Console.WriteLine("HTTP Statuskod: " + (int)response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Ett nätverksfel uppstod vid insättning av produkten.");
                }
                return StatusCode(500, "Ett nätverksfel uppstod vid insättning av produkten.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade: {ex.Message}");
                return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
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
