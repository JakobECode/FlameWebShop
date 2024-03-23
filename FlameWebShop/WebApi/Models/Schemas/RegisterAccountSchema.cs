using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Entities;

namespace WebApi.Models.Schemas
{
    public class RegisterAccountSchema
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(2)]
        public string LastName { get; set; } = null!;
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$")]
        public string Password { get; set; } = null!;
        public string RoleName { get; set; } = "user";

        public static implicit operator IdentityUser(RegisterAccountSchema schema)
        {
            return new IdentityUser
            {
                UserName = schema.Email,
                Email = schema.Email,
            };
        }
        public static implicit operator UserProfileEntity(RegisterAccountSchema schema)
        {
            return new UserProfileEntity
            {
                FirstName = schema.FirstName,
                LastName = schema.LastName,
            };
        }
    }
}
