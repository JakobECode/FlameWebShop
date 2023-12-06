using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = 0;
        public int StarRating { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string Tag { get; set; } = null!;
        //public Category? Category { get; set; }

        // Definierar en implicit konvertering från ProductEntity till Product.
        // Detta gör det möjligt att automatiskt konvertera en ProductEntity-instans till en Product-instans.
        public static implicit operator Product(ProductEntity entity)
        {
            return new Product
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                StarRating = entity.StarRating,
                ImageUrl = entity.ImageUrl,
                //Category = entity.Category,
                Tag = entity.Tag!
            };
        }

        // Definierar en implicit konvertering från Product till ProductEntity.
        public static implicit operator ProductEntity(Product product)
        {
            return new ProductEntity
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                StarRating = product.StarRating,
                CategoryId = product.CategoryId,
                Tag = product.Tag
            };
        }
    }
}
