using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        //public AddressEntity Address { get; set; } = null!;
        public List<OrderItemEntity> Items { get; set; } = null!;
    }
}
