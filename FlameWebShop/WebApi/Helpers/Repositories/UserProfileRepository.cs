using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class UserProfileRepository : Repository<UserProfileEntity>
    {
        public UserProfileRepository(DataContext context) : base(context) 
        {
        }
    }
}
