using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductSchema schema);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(string category);
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductDTO>> GetByNameAsync(string searchCondition);
        Task<IEnumerable<ProductDTO>> GetByPriceAsync(int minPrice, int maxPrice);
        Task<IEnumerable<ProductDTO>> GetBySalesCategoryAsync(string salesCategory);
        Task<IEnumerable<ProductDTO>> GetFilteredProductsAsync(FilterSchema filter);
        Task<bool> UpdateAsync(ProductSchema schema);
        Task<bool> UpdateRatingAsync(Guid productId);
    }
}
