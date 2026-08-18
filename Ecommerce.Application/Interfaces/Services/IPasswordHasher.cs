namespace Ecommerce.Application.Interfaces.Services
{
    public interface IPasswordHasher
    {
        public string HashPassword(string customerPass);

        public bool ComparePasswordHash(string customerPass, string passHash);
    }
}