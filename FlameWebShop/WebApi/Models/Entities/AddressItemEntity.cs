using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Models.Dtos;

namespace WebApi.Models.Entities
{
    public class AddressItemEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(AddressId))]
        public int AddressId { get; set; }

        [Required]
        public AddressEntity Address { get; set; } = null!;

        public ICollection<UserProfileAddressItemEntity> UserProfileAddressItems { get; set; } = new HashSet<UserProfileAddressItemEntity>();


        public static implicit operator AddressItemDto(AddressItemEntity entity)
        {
            return new AddressItemDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Address = entity.Address,
            };
        }
    }
}
