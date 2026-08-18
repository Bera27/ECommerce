using Ecommerce.Application.Interfaces.Services;

namespace Ecommerce.Infrastructure.Services
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public bool ComparePasswordHash(string customerPass, string passHash)
        {
            bool passwordVerify = BCrypt.Net.BCrypt.Verify(customerPass, passHash);

            return passwordVerify;
        }

        public string HashPassword(string customerPass)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(customerPass);

            return hash;
        }
    }
}