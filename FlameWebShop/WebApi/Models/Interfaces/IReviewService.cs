using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IReviewService
    {
        Task<bool> CreateAsync(ReviewSchema schema, string userName);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ReviewDTO>> GetAllAsync();
        Task<IEnumerable<ReviewDTO>> GetByProductId(Guid productId);
    }
}
