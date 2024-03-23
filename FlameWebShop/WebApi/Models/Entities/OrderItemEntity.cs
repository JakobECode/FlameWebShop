namespace WebApi.Models.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double UnitPrice { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
