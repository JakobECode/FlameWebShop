using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Context;
using WebApi.Helpers.Repositories;
using WebApi.Helpers.Services;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DbContexts
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DBFlameShopCS")));
            builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DBFlameIdentity")));
            #endregion

            #region Repositories
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<CategoryRepository>();
            builder.Services.AddScoped<UserProfileRepository>();
            builder.Services.AddScoped<CommentRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CategoryService>();
            #endregion

            #region Authentication
            // Lägger till och konfigurerar ASP.NET Core Identity i tjänstesamlingen.
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Konfigurerar Identity för att kräva att varje användare har en unik e-postadress.
                options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<IdentityContext>()// Specifierar att Entity Framework ska användas för att lagra identitetsdata.
                .AddDefaultTokenProviders();// Lägger till standardtoken-leverantörer för saker som återställning av lösenord.

            // Lägger till och konfigurerar autentiseringstjänster för applikationen.
            builder.Services.AddAuthentication(x =>
            {
                // Anger JWT Bearer som standardautentiseringsschema.
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x => // Lägger till JWT Bearer som autentiseringsmetod.
            {
                // Konfigurerar händelser för JWT Bearer.
                x.Events = new JwtBearerEvents
                {
                    // Händelse som triggas när ett JWT-tokens giltighet har bekräftats.
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };

                // Konfigurerar JWT Bearer inställningar.
                x.RequireHttpsMetadata = true; // Kräver HTTPS för att skicka metadata.
                x.SaveToken = true; // Anger att token ska sparas efter autentisering.
                x.TokenValidationParameters = new TokenValidationParameters // Definierar hur tokens ska valideras.
                {
                    ValidateIssuer = true, // Validerar utfärdaren av token.
                    // Hämtar och validerar utfärdaren från konfigurationen.
                    ValidIssuer = builder.Configuration.GetSection("TokenValidation").GetValue<string>("Issuer")!,
                    ValidateAudience = true,  // Validerar mottagaren av token.
                    // Hämtar och validerar mottagaren från konfigurationen.
                    ValidAudience = builder.Configuration.GetSection("TokenValidation").GetValue<string>("Audience")!,
                    ValidateLifetime = true,// Validerar tokenens giltighetstid.
                    ValidateIssuerSigningKey = true,  // Validerar nyckeln som används för att signera token.
                    IssuerSigningKey = new SymmetricSecurityKey( // Skapar en säkerhetsnyckel baserad på en hemlig nyckel från konfigurationen.
                        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenValidation").GetValue<string>("SecretKey")!))
                };
            });
            /*
                I den här kodsnutten konfigureras två viktiga aspekter av säkerheten i en ASP.NET Core applikation:
                identitetshantering och JWT-baserad autentisering. Först läggs ASP.NET Core Identity till med 
                konfiguration för unika e-postadresser och användning av Entity Framework för att lagra identitetsdata. 
                Därefter ställs autentiseringen in för att använda JWT-tokens, inklusive konfiguration för validering
                av dessa tokens, såsom att verifiera utfärdare, mottagare och signatur
            */

            #endregion

            var app = builder.Build();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}