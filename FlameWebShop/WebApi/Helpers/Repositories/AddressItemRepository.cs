using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class AddressItemRepository : Repository<AddressItemEntity>
    {
        private readonly DataContext _context;
        public AddressItemRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<AddressItemEntity> GetFullAddressItemAsync(Expression<Func<AddressItemEntity, bool>> predicate)
        {

            var address = await _context.Set<AddressItemEntity>().Include("Address").FirstOrDefaultAsync(predicate);
            if (address != null)
                return address;

            return null!;
        }
    }
}
