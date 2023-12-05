namespace WebApp.Models.Dtos
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int StarRating { get; set; } = 0;
        public string ImageUrl { get; set; }
        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public string Tag { get; set; } = null!;
    }
}
