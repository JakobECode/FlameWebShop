using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class ReviewRepository : Repository<ReviewEntity>
    {
        public ReviewRepository(DataContext context): base(context) 
        {
        }
    }
}
