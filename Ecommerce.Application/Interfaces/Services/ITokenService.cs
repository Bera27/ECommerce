using System.Security.Claims;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(Guid customerId, string customerEmail);

        public ClaimsPrincipal ValidateToken(string token);
    }
}