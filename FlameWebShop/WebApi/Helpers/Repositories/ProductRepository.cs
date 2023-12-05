using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApi.Context;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class ProductRepository : Repository<ProductEntity>
    {
        // Det används för att interagera med databasen, specifikt för att hantera produktrelaterade data.
        private readonly DataContext _context;

        // Konstruktor för ProductRepository. Detta anropar bas-klassens konstruktor med DataContext-instansen som argument.
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            try
            {
                var result = await _context.Products.Include("Category").ToListAsync();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
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
}
