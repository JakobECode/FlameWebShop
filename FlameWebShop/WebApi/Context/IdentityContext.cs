using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models.Entities;

namespace WebApi.Context
{
    // Definition av en publik klass "IdentityContext", som ärver från
    // IdentityDbContext<IdentityUser>. IdentityDbContext är en specialisering av DbContext som används i
    // ASP.NET Core Identity för att hantera användarrelaterade data.
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        // Tar emot DbContextOptions för IdentityContext och vidarebefordrar dessa till bas-klassens konstruktor.
        // Detta möjliggör konfiguration av databasanslutningen och andra val för databas kontexten.
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        // En DbSet som representerar tabellen för UserProfileEntity i databasen.
        // Det används för att utföra CRUD-operationer på användarprofiler.
        public DbSet<UserProfileEntity> UserProfiles { get; set; }
    }
}
/*
IdentityContext-klassen är en del av dataåtkomstlagret i en ASP.NET Core applikation som använder 
Identity-systemet för autentisering och användarhantering.

Den ärver från IdentityDbContext<IdentityUser>, vilket är en del av Entity Framework Core 
och är specialiserad för att hantera identitets- och säkerhetsrelaterade entiteter såsom 
användare, roller och anspråk. 

IdentityContext klassen lägger till en DbSet för UserProfileEntity, vilket möjliggör lagring och 
hantering av användarprofiler i databasen.

Konstruktor tar emot och vidarebefordrar DbContextOptions, vilket tillåter konfiguration av databasanslutningen 
och andra relaterade inställningar vid skapandet av instansen.
*/
