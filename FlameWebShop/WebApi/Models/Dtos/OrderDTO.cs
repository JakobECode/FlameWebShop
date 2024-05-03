using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string? OrderStatus { get; set; }
        public string? Email { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
   
        public List<OrderItemEntity> Items { get; set; } = null!;

        public static implicit operator OrderDto(OrderEntity entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                OrderDate = entity.OrderDate,
                Quantity = entity.Quantity,
                OrderStatus = entity.OrderStatus,
               // Address = entity.Address,
                Email = entity.Email,
                Items = entity.Items,
                StreetName = entity.StreetName,
                PostalCode = entity.PostalCode,
                City = entity.City,
                Country = entity.Country,   
            };
        }
    }
}
