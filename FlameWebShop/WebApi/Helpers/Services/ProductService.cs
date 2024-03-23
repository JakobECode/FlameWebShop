using System.Linq.Expressions;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepo;
        private readonly ReviewRepository _reviewRepo;
        private readonly ICategoryService _categoryService;

        public ProductService(ProductRepository productRepo, ReviewRepository reviewRepo, ICategoryService categoryService)
        {
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            try
            {
                var products = await _productRepo.GetAllAsync();
                var dtos = new List<ProductDto>();

                foreach (var entity in products)
                {
                    //ProductDTO product = entity;
                    dtos.Add(entity);
                }

                return dtos;
            }
            catch { }
            return null!;
        }
        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
        {
            try
            {
                var allProducts = await _productRepo.GetAllAsync();
                var products = allProducts.Where(x => x.Category == category);
                var dto = new List<ProductDto>();

                foreach (var entity in products)
                {
                    dto.Add(entity);
                }

                return dto;
            }
            catch { }
            return null!;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            try
            {
                var product = await _productRepo.GetAsync(x => x.Id == id);
                ProductDto dto = product;

                return dto;
            }
            catch { }
            return null!;
        }

        public async Task<IEnumerable<ProductDto>> GetByPriceAsync(int minPrice, int maxPrice)
        {
            try
            {
                var products = await _productRepo.GetListAsync(x => x.Price >= minPrice && x.Price <= maxPrice);
                var dto = new List<ProductDto>();

                foreach (var entity in products)
                {
                    dto.Add(entity);
                }

                return dto;
            }
            catch { }
            return null!;
        }

        public async Task<IEnumerable<ProductDto>> GetByNameAsync(string searchCondition)
        {
            try
            {
                Expression<Func<ProductEntity, bool>> predicate = p => p.Name.ToLower().Contains(searchCondition.ToLower());
                var products = await _productRepo.GetListAsync(predicate);

                return products.Select(p => (ProductDto)p);
            }
            catch { }
            return null!;
        }

        public async Task<bool> CreateAsync(ProductSchema schema)
        {
            try
            {
                var CategoryList = await _categoryService.CheckOrCreateAsync(schema.Category!);
                if (CategoryList)
                {
                    var categoryResult = await _categoryService.CheckOrCreateAsync(schema.Category!);
                    if (categoryResult)
                    {
                        ProductEntity entity = schema;
                        await _productRepo.AddAsync(entity);

                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public async Task<bool> UpdateAsync(ProductSchema schema)
        {
            try
            {
                ProductEntity entity = schema;
                await _productRepo.UpdateAsync(entity);

                return true;
            }
            catch { }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _productRepo.GetAsync(x => x.Id == id);
                await _productRepo.DeleteAsync(entity);

                return true;
            }
            catch { }
            return false;
        }

        public async Task<bool> UpdateRatingAsync(int productId)
        {
            try
            {
                var ratings = new List<double>();
                var product = await _productRepo.GetAsync(p => p.Id == productId);
                var reviews = await _reviewRepo.GetListAsync(r => r.ProductId == productId);

                foreach (var review in reviews)
                {
                    ratings.Add(review.Rating);
                }

                double count = ratings.Count;
                if (count > 0)
                {
                    product.Rating = ratings.Sum() / count;
                    product.ReviewCount = ratings.Count;
                    await _productRepo.UpdateAsync(product);

                    return true;
                }
            }
            catch { }
            return false;
        }

    }
}
