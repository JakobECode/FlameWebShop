namespace WebApi.Models.Schemas
{
    public class OrderItemSchema
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
