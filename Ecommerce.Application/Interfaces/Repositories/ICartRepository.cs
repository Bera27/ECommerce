using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetOrCreateCartAsync(Guid customerId);

        Task UpdateCartAsync(Cart cart);
    }
}