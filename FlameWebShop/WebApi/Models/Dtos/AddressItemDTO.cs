using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class AddressItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public AddressEntity Address { get; set; } = null!;
    }
}
