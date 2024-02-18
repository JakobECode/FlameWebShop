using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Context;
using WebApi.Helpers.Interfaces;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly ProductRepository _productRepo;
        public ProductService(DataContext context, ProductRepository productRepo, CategoryRepository categoryRepo )
        {
            _context = context;
            _productRepo = productRepo;
        }
        public async Task<ProductEntity> AddAsync(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product), "Produkten får inte vara null.");
                }
                ProductEntity entity = product;
                var addedEntity = await _productRepo.AddAsync(entity);
                return addedEntity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade vid tillägg av produkt: {ex.Message}");
                throw;
            }
        }
        public async Task<Product> GetAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            try
            {
                var entity = await _productRepo.GetAsync(expression);
                if (entity != null)
                {
                    Product product = entity;
                    return product;
                }
                else
                {
                    throw new KeyNotFoundException("Produkten hittades inte.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ett undantag inträffade vid hämtning av produkt: {ex.Message}");
                throw; 
            }
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var result = await _productRepo.GetAllAsync();
                var products = new List<Product>();
                if (result != null)
                {
                    foreach (var entity in result)
                    {
                        Product product = entity; 
                        products.Add(product);
                    }
                }
                return products;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
        public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            try
            {
                var result = await _productRepo.GetAllAsync(expression); 
                var products = new List<Product>(); 

                if (result != null)
                {
                    foreach(var entity in result)
                    {
                        Product product = entity;
                        products.Add(product);
                    }
                }
                return products;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _productRepo.GetAsync(x => x.Id == id);
                if (entity == null)
                {
                    return false; 
                }
                await _productRepo.DeleteAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            try
            {
                var resultEntity = await _productRepo.UpdateAsync(product);
                return resultEntity != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Product> InsertAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Misslyckades med att lägga till produkt.", ex);
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