using WebApp.Models.Dtos;

namespace WebApp.Models
{
    public class CollectionItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int StarRating { get; set; } = 0;
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Tag { get; set; } = null!;


        public static implicit operator CollectionItemModel(Product product)
        {
            return new CollectionItemModel()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                StarRating = product.StarRating,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category.CategoryName,
                Tag = product.Tag,
            };
        }
    }
}
