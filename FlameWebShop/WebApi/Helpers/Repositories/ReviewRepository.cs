using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class ReviewRepository : SqlRepository<ReviewEntity>
    {
        public ReviewRepository(SqlContext context): base(context) 
        {
        }
    }
}
