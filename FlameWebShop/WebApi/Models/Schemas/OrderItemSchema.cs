namespace WebApi.Models.Schemas
{
    public class OrderItemSchema
    {
        public Guid ProductId { get; set; }
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
