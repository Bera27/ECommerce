namespace Ecommerce.Application.Responses
{
    public class LoginResponse
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}