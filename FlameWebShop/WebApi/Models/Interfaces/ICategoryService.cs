using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CategorySchema schema);
        Task<bool> DeleteAsync(int id);
        Task<bool> CheckOrCreateAsync(string category);
    }
}
