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
        


        public static implicit operator ProductEntity(ProductSchema schema)
        {

          

            return new ProductEntity
            {
                Name = schema.Name,
                Price = schema.Price,
                ImageUrl = schema.ImageUrl,
                Category = schema.Category,
                Description = schema.Description,
            };
        }
    }
}
