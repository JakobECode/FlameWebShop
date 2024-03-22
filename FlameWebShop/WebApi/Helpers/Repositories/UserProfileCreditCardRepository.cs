using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class UserProfileCreditCardRepository : Repository<UserProfileCreditCardEntity>
    {
        public UserProfileCreditCardRepository(DataContext context) : base(context) 
        { 
        }
    }
}
