using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;
        
        public OrderRepository(EcommerceDbContext context)
            => _context = context;

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(Guid customerId)
        {
            var orders = await _context.Orders
                            .AsNoTracking()
                            .Include(x => x.Items)
                            .Where(x => x.CustomerId == customerId)
                            .OrderByDescending(x => x.OrderDate)
                            .ToListAsync();

            return orders;               
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            var order = await _context.Orders
                            .AsNoTracking()
                            .Include(x => x.Items)
                            .FirstOrDefaultAsync(x => x.Id == orderId);

            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}