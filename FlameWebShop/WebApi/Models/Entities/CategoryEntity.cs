using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class CategoryEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;
    }
}
