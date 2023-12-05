using WebApi.Context;

namespace WebApi.Helpers.Services
{
    public class CategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        /* 
           Detta används för att interagera med databasen och utföra CRUD-operationer.
           Konstruktor för CategoryService, 
           den tar en instans av DataContext som parameter och använder den för att initialisera _context-fältet.
           DataContext är en del av Entity Framework och används för att interagera med databasen.
           Tack vare injektionen av DataContext genom konstruktorn blir klassen lättare att testa och underhålla, 
           eftersom den inte skapar egna instanser av databaskontexten utan förlitar sig på en extern källa
        */

    }
}
