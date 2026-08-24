namespace Ecommerce.Application.Responses
{
    public class RegisterCustomerResponse
    {
        public Guid Id { get; set; } 
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}