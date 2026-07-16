using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public required string NameProduct { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public void ApplyDiscount(decimal discount)
        {
            if (discount > 80 || discount < 1)
            {
                throw new DomainException($"Não é possível aplicar um desconto maior que 80% ou menor que 1%. Desconto selecionado: {discount}");
            }

            UnitPrice -= (UnitPrice * discount) / 100m;
        }
    }
}