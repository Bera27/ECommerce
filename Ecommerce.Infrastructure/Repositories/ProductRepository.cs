using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;
        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                        .AsNoTracking()
                        .Include(x => x.Images)
                        .Where(x => x.IsActive)
                        .ToListAsync();

            return products;
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            var product = await _context.Products
                        .AsNoTracking()
                        .Include(x => x.Images)
                        .Where(x => x.Id == productId)
                        .FirstOrDefaultAsync();

            if(product == null)
                return null;

            return product;
        }

        public async Task<bool> RemoveProductAsync(Guid productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            if(product == null)
                return false;

            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}