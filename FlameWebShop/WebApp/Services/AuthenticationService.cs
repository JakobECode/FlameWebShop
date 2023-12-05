using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApp.Services
{
    public class AuthenticationService
    {
        private IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsTokenValidAndContainsRole(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenValidation").GetValue<string>("SecretKey")!));

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "WebApi",
                    ValidAudience = "AuthenticatedUsers",
                    IssuerSigningKey = key
                };

                var handler = new JwtSecurityTokenHandler();
                var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var validatedToken);

                var roles = claimsPrincipal.FindAll(ClaimTypes.Role);
                return roles.Any(r => r.Value == "Admin" || r.Value == "Produktansvarig");
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }
    }
}
