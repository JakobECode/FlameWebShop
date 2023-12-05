using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Helpers
{
    public class TokenGenerator
    {
        // Ett statiskt readonly fält för att hålla konfigurationsinställningar.
        private static readonly IConfiguration _configuration;

        // Statisk konstruktor för att initialisera _configuration fältet.
        // Läser konfigurationsinställningarna från "appsettings.json".
        static TokenGenerator()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        // En statisk metod för att generera en JWT baserat på användaranspråk (claims) och utgångstiden.
        public static string Generate(ClaimsIdentity claims, DateTime expiresAt)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // Skapar en ny instans av JwtSecurityTokenHandler.
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetSection("TokenValidation").GetValue<string>("Issuer")!, // Hämtar utfärdaren (issuer) från konfigurationen.
                Audience = _configuration.GetSection("TokenValidation").GetValue<string>("Audience")!, // Hämtar mottagaren (audience) från konfigurationen.
                Subject = claims,  // Anger användaranspråk (claims) för token.
                Expires = expiresAt, // Anger utgångstiden för token.
                SigningCredentials = new SigningCredentials(  // Skapar signeringsuppgifter för token.
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenValidation").GetValue<string>("SecretKey")!)), // Använder en hemlig nyckel från konfigurationen.
                    SecurityAlgorithms.HmacSha512Signature) // Anger vilken signaturalgoritm som ska användas.
            };
            // Skapar JWT och returnerar den som en sträng.
            return tokenHandler.WriteToken(tokenHandler.CreateToken(securityTokenDescriptor));
        }
    }
}
/*
   TokenGenerator-klassen är ansvarig för att generera JWT (JSON Web Tokens), som ofta används i moderna 
   webbapplikationer för autentisering och auktorisering. 
   Den läser konfigurationsinställningar från "appsettings.json" för att hämta information som behövs för 
   att skapa token, såsom utfärdarens (issuer) och mottagarens (audience) identifierare samt en hemlig nyckel 
   för signering. Metoden Generate tar emot användaranspråk (claims) och en utgångstid för att skapa och 
   returnera en JWT. Denna token kan sedan användas i autentiseringsprocessen för att verifiera och auktorisera 
   användare.
 */