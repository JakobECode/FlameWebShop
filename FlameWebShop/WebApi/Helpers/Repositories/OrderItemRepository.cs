using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class OrderItemRepository : Repository<OrderItemEntity>
    {
        public OrderItemRepository(DataContext context) : base(context)
        {
        }
    }
}
