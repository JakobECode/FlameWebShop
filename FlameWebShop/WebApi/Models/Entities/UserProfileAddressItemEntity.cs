using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Entities
{
    [PrimaryKey(nameof(UserProfileId))]
    public class UserProfileAddressItemEntity
    {
        [Required]
        public string UserProfileId { get; set; } = null!;

        public UserProfileEntity UserProfile { get; set; } = null!;

        [Required]
        public int AddressItemId { get; set; }

        public AddressItemEntity AddressItem { get; set; } = null!;
    }
}
