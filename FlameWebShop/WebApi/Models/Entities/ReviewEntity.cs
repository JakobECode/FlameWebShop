using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class ReviewEntity
    {
        [Key]
        [Required]
        public int Id { get; set; } 

        public int ProductId { get; set; }
        public string UserId { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Comment { get; set; } = null!;

        [Required]
        public double Rating { get; set; }

        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
