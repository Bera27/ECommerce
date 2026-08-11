namespace Ecommerce.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
    }
}