using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class CategoryRepository : Repository<CategoryEntity>
    {
        public CategoryRepository(DataContext context) : base(context) 
        {
        }
    }
}
