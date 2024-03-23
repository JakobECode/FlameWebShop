using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Category { get; set; }
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Discount { get; set; }
        public double? DiscountMultiplier { get; set; }


        public static implicit operator ProductDto(ProductEntity entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Category = entity.Category,
                Description = entity.Description,
                Rating = entity.Rating,
                ReviewCount = entity.ReviewCount,
                CreatedDate = entity.CreatedDate,
                Discount = entity.Discount,
                DiscountMultiplier = entity.DiscountMultiplier,
            };
        }
    }
}
