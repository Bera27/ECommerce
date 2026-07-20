using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid orderId);

        Task<Order> CreateOrderAsync(Order order);

        Task<IEnumerable<Order>> GetAllAsync(Guid customerId);

        Task UpdateOrderAsync(Order order);
    }
}