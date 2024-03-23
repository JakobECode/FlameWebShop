using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        [Required]
        public int Id { get; set; } 

        public string? Category { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = null!;

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Range(0, 5)]
        public double Rating { get; set; } = 0;

        public int ReviewCount { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Discount { get; set; }
        public double? DiscountMultiplier { get; set; }
    }
}
