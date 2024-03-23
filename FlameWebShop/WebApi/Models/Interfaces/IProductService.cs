using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetByPriceAsync(int minPrice, int maxPrice);
        Task<IEnumerable<ProductDto>> GetByNameAsync(string searchCondition);
        Task<bool> CreateAsync(ProductSchema schema);
        Task<bool> UpdateAsync(ProductSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateRatingAsync(int productId);
    }
}
