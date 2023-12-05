using System.Diagnostics;
using WebApi.Context;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class UserProfileRepository
    {
        private readonly IdentityContext _context;
        public UserProfileRepository(IdentityContext context)
        {
            _context = context;
        }
        public async Task<UserProfileEntity>AddAsync(UserProfileEntity entity)
        {
            try
            {
                _context.UserProfiles.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return entity;
        }

        /*
         Denna klass är en del av datalagret i applikationen och hanterar operationer relaterade 
         till användarprofiler i en databas.
         En privat readonly-fält av typen IdentityContext. 
         Det används för att interagera med databasen, specifikt för att hantera användarprofiler.
         Konstruktor för UserProfileRepository. Tar en IdentityContext-instans och initialiserar _context-fältet.
         IdentityContext är en del av Entity Framework och används för att interagera med databasen.
         En asynkron metod för att lägga till en användarprofil i databasen.
         Fångar undantag och skriver ut felmeddelandet i debug-konsolen.
         Returnerar den tillagda användarprofilen.
         */
    }
}
