using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class OrderEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public string? UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public string? OrderStatus { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [StringLength(100, ErrorMessage = "Street name cannot exceed 100 characters")]
        public string? StreetName { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code")]
        public string? PostalCode { get; set; }

        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "Country name cannot exceed 50 characters")]
        public string? Country { get; set; }
    }
}