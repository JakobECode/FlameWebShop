namespace WebApi.Models.Schemas
{
    public class OrderSchema
    {
        
        public int Id { get; set; }
        public List<OrderItemSchema> Items { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Email { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
