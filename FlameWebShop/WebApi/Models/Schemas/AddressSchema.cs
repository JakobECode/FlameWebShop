using System.ComponentModel.DataAnnotations;
using WebApi.Models.Entities;

namespace WebApi.Models.Schemas
{
    public class AddressSchema
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(2)]
        public string StreetName { get; set; } = null!;

        [Required]
        public string PostalCode { get; set; } = null!;

        [Required]
        [MinLength(2)]
        public string City { get; set; } = null!;

        [Required]
        [MinLength(2)]
        public string Country { get; set; } = null!;


        public static implicit operator AddressEntity(AddressSchema schema)
        {
            return new AddressEntity
            {
                StreetName = schema.StreetName,
                PostalCode = schema.PostalCode,
                City = schema.City,
                Country = schema.Country,
            };
        }
    }
}
