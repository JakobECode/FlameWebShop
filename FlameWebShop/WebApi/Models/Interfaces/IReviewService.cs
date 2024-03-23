using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllAsync();
        Task<IEnumerable<ReviewDto>> GetByProductId(int productId);
        Task<bool> CreateAsync(ReviewSchema schema, string userName);
        Task<bool> DeleteAsync(int id);
    }
}
