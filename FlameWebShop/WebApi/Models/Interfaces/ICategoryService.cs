using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategorySchema schema);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(Guid id);
        Task<bool> CheckOrCreateAsync(string category);

    }
}
