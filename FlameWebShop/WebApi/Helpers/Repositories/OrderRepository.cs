using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class OrderRepository : Repository<OrderEntity>
    {
        public OrderRepository(DataContext context) : base(context) 
        {
        }
    }
}
