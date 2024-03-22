using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class UserProfileAddressItemRepository : Repository<UserProfileAddressItemEntity>
    {
        public UserProfileAddressItemRepository(DataContext context) : base(context) 
        {
        }
    }
}
