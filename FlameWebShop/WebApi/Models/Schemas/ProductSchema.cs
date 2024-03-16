using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Entities;

namespace WebApi.Models.Schemas
{
    public class ProductSchema
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required]
        public string ImageUrl { get; set; } = null!;
        public string? Category { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Brand { get; set; } = null!;
        public string? SalesCategory { get; set; }


        public static implicit operator ProductEntity(ProductSchema schema)
        {

            var salesCat = schema.SalesCategory.IsNullOrEmpty() ? "New" : schema.SalesCategory;

            return new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = schema.Name,
                Price = schema.Price,
                ImageUrl = schema.ImageUrl,
                Description = schema.Description,
                Brand = schema.Brand,
                Category = schema.Category,
                SalesCategory = salesCat,
            };
        }
    }
}
