using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EcommerceDbContext _context;
        public CustomerRepository(EcommerceDbContext context)
        {
            _context = context;    
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var customer = await _context.Customers
                            .AsNoTracking()
                            .Include(x => x.Addresses)
                            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            return customer;
        }

        public async Task<bool> RemoveCustomerAsync(Guid customerId)
        {
            var customer = await _context.Customers
                            .FirstOrDefaultAsync(x => x.Id == customerId);

            if(customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}