namespace Ecommerce.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public IList<CartItem> Items { get; set; } = [];
    }
}