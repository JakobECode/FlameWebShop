using WebApi.Context;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class CommentRepository : Repository<CommentEntity>
    {
        public CommentRepository(DataContext context) : base(context)
        { 
        }
    }

    /*
      CommentRepository-klassen är en specialisering av den generiska Repository-klassen
      för att hantera CommentEntity-objekt. Det är en del av datalagret och erbjuder en 
      samling av standardiserade metoder (ärvt från Repository<CommentEntity>) för CRUD-operationer
      på kommentarer i databasen. Konstruktorn tar emot en DataContext-instans som den skickar vidare 
      till bas-klassens konstruktor, vilket är viktigt för att etablera anslutningen till databasen och 
      göra det möjligt att använda bas-klassens funktionalitet. Om det finns behov av ytterligare 
      anpassade metoder specifika för hantering av kommentarer, kan dessa definieras inom denna klass.
    */
}
