using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class ProductRepository : Repository<ProductEntity>
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Products.FirstOrDefaultAsync(expression);
                return entity!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }
        public override async Task<IEnumerable<ProductEntity>> GetAllAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            try
            {
                var result = await _context.Products.Where(expression).ToListAsync();

                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
        public override async Task<ProductEntity> AddAsync(ProductEntity entity)
        {
            try
            {
                var result = await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }
        public override async Task<ProductEntity> UpdateAsync(ProductEntity entity)
        {
            try
            {
                _context.Products.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return entity;
            }
        }
        public override async Task<bool> DeleteAsync(ProductEntity entity)
        {
            try
            {
                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public override async Task<ProductEntity> InsertAsync(ProductEntity entity)
        {
            try
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
/*
 ProductRepository-klassen är en specialisering av den generiska Repository-klassen för att
 hantera ProductEntity-objekt. Det är en del av datalagret och erbjuder en samling av metoder för 
 CRUD-operationer på produkter i databasen. Denna klass överskuggar vissa metoder från bas-klassen 
 (GetAllAsync, GetAsync) för att inkludera den relaterade Category-entiteten när produktdata hämtas. 
 Detta är ett vanligt mönster i dataåtkomstlagret för att tillhandahålla mer detaljerade uppgifter 
 om entiteter och deras relationer. Användningen av Include-metoden är en del av Entity Framework 
 för att hantera laddning av relaterade data (kallas eager loading). Exceptionhantering och asynkron 
 programmering används för att förbättra applikationens robusthet och prestanda.
 */
