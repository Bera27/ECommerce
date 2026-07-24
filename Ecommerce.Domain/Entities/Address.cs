using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public BrazilianState State { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string Neighborhood { get; set; }
        public string Complement { get; set; } = null!;
        public required string Number { get; set; }
        public required string ZipCode { get; set; }
    }
}