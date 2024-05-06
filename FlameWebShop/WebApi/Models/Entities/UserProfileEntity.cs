using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class UserProfileEntity
    {
        [Key, ForeignKey(nameof(UserId))]
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public ICollection<UserProfileCreditCardEntity> UserProfileCreditCards { get; set; } = new HashSet<UserProfileCreditCardEntity>();
    }
}
