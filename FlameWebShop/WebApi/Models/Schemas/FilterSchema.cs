namespace WebApi.Models.Schemas
{
    public class FilterSchema
    {
        public string? Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Category { get; set; }
        public string? SalesCategory { get; set; }
        public int? Amount { get; set; }
    }
}
