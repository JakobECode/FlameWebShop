using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Schemas
{
    public class ConfirmPhoneSchema
    {
        [Required]
        [RegularExpression(@"^\+\d{1,3}\d{9}$", ErrorMessage = "Not a valid phonenumber")]
        public string Phone { get; set; } = null!;
    }
}
