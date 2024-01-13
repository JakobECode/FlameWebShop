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
            // L�gger till och konfigurerar ASP.NET Core Identity i tj�nstesamlingen.
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Konfigurerar Identity f�r att kr�va att varje anv�ndare har en unik e-postadress.
                options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<IdentityContext>()// Specifierar att Entity Framework ska anv�ndas f�r att lagra identitetsdata.
                .AddDefaultTokenProviders();// L�gger till standardtoken-leverant�rer f�r saker som �terst�llning av l�senord.

            // L�gger till och konfigurerar autentiseringstj�nster f�r applikationen.
            builder.Services.AddAuthentication(x =>
            {
                // Anger JWT Bearer som standardautentiseringsschema.
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x => // L�gger till JWT Bearer som autentiseringsmetod.
            {
                // Konfigurerar h�ndelser f�r JWT Bearer.
                x.Events = new JwtBearerEvents
                {
                    // H�ndelse som triggas n�r ett JWT-tokens giltighet har bekr�ftats.
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };

                // Konfigurerar JWT Bearer inst�llningar.
                x.RequireHttpsMetadata = true; // Kr�ver HTTPS f�r att skicka metadata.
                x.SaveToken = true; // Anger att token ska sparas efter autentisering.
                x.TokenValidationParameters = new TokenValidationParameters // Definierar hur tokens ska valideras.
                {
                    ValidateIssuer = true, // Validerar utf�rdaren av token.
                    // H�mtar och validerar utf�rdaren fr�n konfigurationen.
                    ValidIssuer = builder.Configuration.GetSection("TokenValidation").GetValue<string>("Issuer")!,
                    ValidateAudience = true,  // Validerar mottagaren av token.
                    // H�mtar och validerar mottagaren fr�n konfigurationen.
                    ValidAudience = builder.Configuration.GetSection("TokenValidation").GetValue<string>("Audience")!,
                    ValidateLifetime = true,// Validerar tokenens giltighetstid.
                    ValidateIssuerSigningKey = true,  // Validerar nyckeln som anv�nds f�r att signera token.
                    IssuerSigningKey = new SymmetricSecurityKey( // Skapar en s�kerhetsnyckel baserad p� en hemlig nyckel fr�n konfigurationen.
                        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenValidation").GetValue<string>("SecretKey")!))
                };
            });
            /*
                I den h�r kodsnutten konfigureras tv� viktiga aspekter av s�kerheten i en ASP.NET Core applikation:
                identitetshantering och JWT-baserad autentisering. F�rst l�ggs ASP.NET Core Identity till med 
                konfiguration f�r unika e-postadresser och anv�ndning av Entity Framework f�r att lagra identitetsdata. 
                D�refter st�lls autentiseringen in f�r att anv�nda JWT-tokens, inklusive konfiguration f�r validering
                av dessa tokens, s�som att verifiera utf�rdare, mottagare och signatur
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