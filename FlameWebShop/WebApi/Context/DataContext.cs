using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models.Entities;

namespace WebApi.Context
{
    public class DataContext : DbContext
    {
        // Tar emot DbContextOptions för DataContext och vidarebefordrar dessa till bas-klassens konstruktor.
        // Detta möjliggör konfiguration av databasanslutningen och andra val för databas kontexten.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {        
        }

        //IConfiguration appConfig;
        //public DataContext (IConfiguration config)
        //{
        //    appConfig = config;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   // optionsBuilder.UseSqlServer("server=.;database=DBFlameWebShop;trusted_connection=true;TrustServerCertificate=true");
        //    optionsBuilder.UseSqlServer(appConfig.GetConnectionString("DBFlameShopCS"));
           
        //}

        // En DbSet som representerar tabellen för ProductEntity i databasen.
        // Det används för att utföra CRUD-operationer på produktdata.
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
/*
DataContext-klassen är en del av dataåtkomstlagret i en applikation som använder Entity Framework Core. 
Den ärver från DbContext, vilket är kärnan i Entity Framework och ger funktionalitet för att interagera med en databas.
Klassen definierar flera DbSet-egenskaper, var och en representerar en tabell i databasen. 
Dessa DbSet-egenskaper (Products, Categories, Comments) används för att genomföra CRUD-operationer 
(Skapa, Läsa, Uppdatera, Radera) på respektive entitetstyper (ProductEntity, CategoryEntity, CommentEntity). 
Konstruktorn tar emot DbContextOptions, vilket möjliggör konfiguration av databasanslutningen och andra 
relaterade inställningar vid skapandet av instansen.
*/
