using WebApi.Models.Dtos;
using WebApi.Models.Entities;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IAddressService
    {
        Task<bool> DeleteAddressAsync(int addressItemId);
        Task<List<AddressItemDTO>> GetUserAddressesAsync(string userName);
        Task<bool> RegisterAddressAsync(RegisterAddressSchema schema, string userName);
        Task<AddressItemEntity> UpdateAddressAsync(UpdateAddressSchema schema, string userName);
    }
}
