using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Schemas
{
    public class ResetPasswordSchema
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
