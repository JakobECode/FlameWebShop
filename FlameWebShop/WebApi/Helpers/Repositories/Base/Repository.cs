using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Context;

namespace WebApi.Helpers.Repositories.Base
{
    public abstract class Repository<TEntity> where TEntity : class
    {
        // privat readonly-fält av typen DataContext, det används för att interagera med databasen och utföra CRUD-operationer.
        private readonly DataContext _context;

        // Konstruktor för Repository. Tar en DataContext-instans och initialiserar _context-fältet.
        protected Repository(DataContext context)
        {
            _context = context;
        }

        // Metod för att lägga till en entitet i databasen asynkront,
        // fångar undantag och skriver ut felmeddelandet i debug-konsolen.
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return entity;
        }
        // Metod för att kontrollera om någon entitet uppfyller ett visst villkor asynkront.
        // Returnerar true om villkoret uppfylls, annars false.
        public virtual async Task<bool> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await _context.Set<TEntity>().AnyAsync(expression);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }

        // Metod för att hämta en entitet baserat på ett villkor asynkront.
        // Returnerar den första entiteten som uppfyller villkoret, eller null om ingen hittas.
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                return entity!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        // Metod för att hämta alla entiteter i databasen asynkront.
        // Returnerar en lista av entiteter.
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        // Metod för att hämta alla entiteter som uppfyller ett visst villkor asynkront.
        // Returnerar en lista av entiteter som uppfyller villkoret.
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await _context.Set<TEntity>().Where(expression).ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        // Metod för att uppdatera en befintlig entitet i databasen asynkront.
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return entity;
        }

        // Metod för att ta bort en entitet från databasen asynkront.
        //Returnerar true om borttagningen lyckades, annars false.
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
        /*
        Denna klass är en generisk abstrakt klass som representerar en generisk repository för att hantera 
        databasoperationer. 
        Den erbjuder grundläggande CRUD-funktionalitet (Skapa, Läsa, Uppdatera, Radera) för vilken som helst typ
        av entitet (TEntity) i en databas. 
        _context-fältet är en instans av DataContext som används för alla databasinteraktioner. 
        Metoderna är märkta med virtual, vilket innebär att de kan överskuggas av subklasser om anpassad 
        funktionalitet krävs. Användningen av asynkrona metoder (async och await) säkerställer att databasoperationerna 
        hanteras effektivt utan att blockera huvudtråden.
        */
    }
}
