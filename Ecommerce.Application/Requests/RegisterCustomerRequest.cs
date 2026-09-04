using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Requests
{
    public class RegisterCustomerRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string State { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string Neighborhood { get; set; }
        public string? Complement { get; set; }
        public required string Number { get; set; }
        public required string ZipCode { get; set; }
    }
}