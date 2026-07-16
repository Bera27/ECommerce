using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public BrazilianState State { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string Neighborhood { get; set; }
        public required string Complement { get; set; }
        public required string Number { get; set; }
        public required string ZipCode { get; set; }
    }
}