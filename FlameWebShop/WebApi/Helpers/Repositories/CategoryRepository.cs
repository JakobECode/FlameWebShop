using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class CategoryRepository : SqlRepository<CategoryEntity>
    {
        public CategoryRepository(SqlContext context) : base(context) 
        {
        }
    }
}
