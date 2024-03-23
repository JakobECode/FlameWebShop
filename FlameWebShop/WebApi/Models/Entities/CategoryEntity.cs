using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class CategoryEntity
    {
        [Key]
        [Required]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; } = null!;
    }
}
