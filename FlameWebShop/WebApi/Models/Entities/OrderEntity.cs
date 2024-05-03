using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string? OrderStatus { get; set; }
        public string? Email { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public List<OrderItemEntity> Items { get; set; } = null!;
    }
}
