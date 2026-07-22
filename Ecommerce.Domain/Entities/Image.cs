namespace Ecommerce.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string Type { get; set; }
    }
}