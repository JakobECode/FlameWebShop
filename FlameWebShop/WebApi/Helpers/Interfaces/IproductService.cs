using System.Linq.Expressions;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Interfaces
{
    public interface IProductService
    {
        Task<ProductEntity> AddAsync(Product product);
        Task<Product> GetAsync(Expression<Func<ProductEntity, bool>> expression);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllAsync(Expression<Func<ProductEntity, bool>> expression);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Product product);
        Task<Product> InsertAsync(Product product);

    }
}
