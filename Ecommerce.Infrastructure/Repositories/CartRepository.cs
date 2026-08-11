using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceDbContext _context;

        public CartRepository(EcommerceDbContext context)
            => _context = context;

        public async Task<Cart> GetOrCreateCartAsync(Guid customerId)
        {
            var findCart = await _context.Carts
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if(findCart != null)
                return findCart;

            var newCart = new Cart
            {
               CustomerId = customerId, 
            };

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            return newCart;
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}