namespace WebApi.Models.Schemas
{
    public class OrderItemSchema
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
