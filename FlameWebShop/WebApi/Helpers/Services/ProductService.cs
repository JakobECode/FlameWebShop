using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Context;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Services
{
    public class ProductService
    {
        // Privata fält som lagrar referenser till DataContext, ProductRepository och CategoryRepository.
        // Dessa används för att hantera databasåtkomst och CRUD-operationer relaterade till produkter och kategorier.

        private readonly DataContext _context;
        private readonly ProductRepository _productRepo;
        private readonly CategoryRepository _categoryRepo;

        // Konstruktor för ProductService. Initialiserar fälten med de instanser som förmedlas vid instansiering.
        // Denna beroendeinjektion gör det enkelt att byta ut och testa olika delar av systemet.

        public ProductService(DataContext context, ProductRepository productRepo, CategoryRepository categoryRepo )
        {
            _context = context;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<ProductEntity> AddAsync(Product product)
        {
            ProductEntity entity = product;
            return await _productRepo.AddAsync(entity);
        }

        // Asynkron metod för att hämta en specifik produkt baserat på ett givet villkor.
        // Använder ProductRepository för att hitta produkten och konverterar den till Product.
        public async Task<Product> GetAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            var entity = await _productRepo.GetAsync(expression); // Hämtar produktentiteten baserat på villkoret.
            Product product = entity; // Konverterar ProductEntity till Product.
            return product; // Returnerar produkten.
        }

        // En asynkron metod för att hämta alla produkter.
        // Använder ProductRepository för att utföra operationen och konverterar resultatet till en lista av Product.
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var result = await _productRepo.GetAllAsync();// Hämtar alla produkter.
                var products = new List<Product>(); // Skapar en lista för att lagra de konverterade produkterna.
                if (result != null)
                {
                    foreach (var entity in result)
                    {
                        Product product = entity;  // Konverterar varje ProductEntity till Product.
                        products.Add(product);
                    }
                }
                return products;  // Returnerar listan av produkter.
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }// Skriver ut felmeddelandet vid undantag.
            return null!;
        }

        // En asynkron metod för att hämta alla produkter som uppfyller ett visst villkor.
        // Använder ProductRepository för att utföra operationen och konverterar resultatet till en lista av Product.

        public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            try
            {
                var result = await _productRepo.GetAllAsync(expression); // Hämtar produkter baserat på villkoret.
                var products = new List<Product>(); // Skapar en lista för att lagra de konverterade produkterna.

                if (result != null)
                {
                    foreach(var entity in result)
                    {
                        Product product = entity;
                        products.Add(product);
                    }
                }
                return products; // Returnerar listan av produkter.
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }  // Skriver ut felmeddelandet vid undantag.
            return null!;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _productRepo.GetAsync(x => x.Id == id); // Hämtar produktentiteten baserat på ID.
                if (entity == null)
                {
                    return false; // Returnerar false om ingen produkt hittades.
                }
                await _productRepo.DeleteAsync(entity); // Tar bort produktentiteten.
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // Skriver ut felmeddelandet vid undantag.
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (id != product.Id)
            {
                // Hantera fallet där id inte matchar product.Id
                return false;
            }

            try
            {
                var existingEntity = await _productRepo.GetAsync(x => x.Id == product.Id);
                if (existingEntity == null)
                {
                    // Loggar om produkten inte hittades
                    // Log.Information($"Produkt med ID {product.Id} hittades inte.");
                    return false;
                }


                var resultEntity = await _productRepo.UpdateAsync(product);
                return resultEntity != null;
            }
            catch (Exception ex)
            {
                // Loggar undantaget
                // Log.Error(ex, $"Ett undantag inträffade vid uppdatering av produkt: {ex.Message}");
                return false;
            }
        }

    }
}
/*
  ProductService-klassen fungerar som en del av affärslogiklagret och koordinerar interaktioner 
  mellan databasen och applikationen. 
  Den använder ProductRepository och CategoryRepository för att utföra databasoperationer. 
  Metoderna i denna klass hanterar skapande, hämtning och filtrering av ProductEntity-objekt, 
  och de konverterar dessa objekt till Product-instanser som sedan kan användas i andra delar 
  av applikationen. Asynkron programmering används för att förbättra prestanda och skalbarhet. 
  Exceptionhantering säkerställer att eventuella fel hanteras och loggas på ett lämpligt sätt.
 */