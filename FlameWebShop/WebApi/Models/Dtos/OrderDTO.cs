using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public AddressEntity? Address { get; set; }
        public decimal Price { get; set; }
        public List<OrderItemEntity> Items { get; set; } = null!;

        public static implicit operator OrderDto(OrderEntity entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                OrderDate = entity.OrderDate,
                OrderStatus = entity.OrderStatus,
                Address = entity.Address,
                Items = entity.Items,
                Price = entity.Price,
            };
        }
    }
}
