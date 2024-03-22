using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class CreditCardRepository : Repository<CreditCardEntity>
    {
        public CreditCardRepository(DataContext context) : base(context)
        {
        }

    }
}
