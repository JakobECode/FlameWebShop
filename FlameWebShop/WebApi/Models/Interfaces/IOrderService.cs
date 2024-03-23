using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetBySignedInUser(string userEmail);
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId);
        Task<bool> CancelOrder(OrderCancelSchema schema);
        Task<bool> CreateOrderAsync(OrderSchema schema, string userEmail);
        Task<bool> DeleteOrder(int orderId);
    }
}
