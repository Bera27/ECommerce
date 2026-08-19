using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
            => _configuration = configuration;

        public string GenerateToken(Guid customerId, string customerEmail)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, customerEmail),
                new(ClaimTypes.NameIdentifier, customerId.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = GetSigningKeyHelper();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = GetSigningKeyHelper();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(
                token,
                validationParameters,
                out SecurityToken validatedToken
            );

            return principal;
        }

        private byte[] GetSigningKeyHelper()
        {
             string? jwtKeyConfig = _configuration["Jwt:Key"];

            if(string.IsNullOrEmpty(jwtKeyConfig))
                throw new InvalidOperationException("A chave JWT não está configurada.");

            var key = Encoding.ASCII.GetBytes(jwtKeyConfig);

            return key;
        }
    }
}