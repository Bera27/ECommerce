using System.Security.Claims;
using Ecommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure.Tests.Services
{
    public class TokenServiceTest
    {
        [Fact]
        public void GenerateAndValidateToken_ClaimsMatchOriginalData_ReturnsValidPrincipal()
        {
            var expectedId = Guid.NewGuid();
            var expectedEmail = "g.bera2710@gmail.com";

            var configValues = new Dictionary<string, string?>
            {
                {"Jwt:Key", "fhfuifhfJAJJJJDJA!!*@*$*!@2184421"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

            var tokenService = new TokenService(configuration);

            var customerToken = tokenService.GenerateToken(expectedId, expectedEmail);
            
            ClaimsPrincipal principal = tokenService.ValidateToken(customerToken);

            var actualId = principal.FindFirst(ClaimTypes.NameIdentifier);
            var actualEmail = principal.FindFirst(ClaimTypes.Email);

            Assert.NotNull(principal);
            Assert.Equal(expectedEmail, actualEmail?.Value);
            Assert.Equal(expectedId.ToString(), actualId?.Value);     
        }

        [Fact]
        public void GenerateToken_KeyJwtAbsent_ThrowsInvalidOperationException()
        {
            var configValues = new Dictionary<string, string?>();

            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();
        
            var tokenService = new TokenService(configuration);

            Assert.Throws<InvalidOperationException>(() => tokenService.GenerateToken(Guid.NewGuid(), "gabriel@gmail.com"));
        }
    }
}