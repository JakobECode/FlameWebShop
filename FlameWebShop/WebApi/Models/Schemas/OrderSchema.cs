namespace WebApi.Models.Schemas
{
    public class OrderSchema
    {
       // public int AddressId { get; set; }
        public List<OrderItemSchema> Items { get; set; } = null!;
        public decimal Price { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
