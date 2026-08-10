using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByEmailAsync(string email);

        Task<Customer> CreateCustomerAsync(Customer customer);

        Task UpdateCustomerAsync(Customer customer);

        Task<bool> RemoveCustomerAsync(Guid customerId);
    }
}