using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public int Quantity { get; set; }
        public required string Description { get; set; }

        public void EnsureStockAvailable(int quantity)
        {
            if (quantity > Quantity)
            {
                throw new DomainException(
                    $"Estoque insuficiente para o produto {Name}. Disponível: {Quantity}, solicitado: {quantity}.");
            }
        }
    }
}