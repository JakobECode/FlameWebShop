using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class ProductRepository : Repository<ProductEntity>
    {
        public ProductRepository(DataContext context) : base(context) 
        {
        }
    }
}
