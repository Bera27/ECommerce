using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data.Mappings;
using System.Reflection;

namespace Ecommerce.Infrastructure.Data
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}