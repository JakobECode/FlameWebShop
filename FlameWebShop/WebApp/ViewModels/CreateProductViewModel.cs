using System.ComponentModel.DataAnnotations;
using WebApp.Models.Dtos;

namespace WebApp.ViewModels
{
    public class CreateProductViewModel
    {

        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public int StarRating { get; set; } = 0;
        public int? SelectedCategory { get; set; }
        public List<Category>? Categories { get; set; }

        public string? SelectedTag { get; set; }
        public List<string>? Tags { get; set; } = new List<string> { "Featured", "New", "Popular" };


        public static implicit operator Product(CreateProductViewModel model)
        {
            return new Product()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                StarRating = model.StarRating,
                ImageUrl = model.ImageUrl,
                CategoryId = model.SelectedCategory,
                Tag = model.SelectedTag!
            };
        }
    }
}
