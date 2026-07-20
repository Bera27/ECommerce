using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid productId);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> CreateProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task RemoveProductAsync(Guid productId);

    }
}