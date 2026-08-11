using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Tests.Repositories
{
    public class CartRepositoryTest
    { 
        [Fact]
        public async Task GetOrCreateCartAsync_CartExists_ReturnCart()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Gabriel",
                Email = "g.bera2710@gmail.com",
                PasswordHash = "122345"
            };

            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Tenis",
                Description = "Nike"
            };

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = product.Id
            };

            await context.AddRangeAsync(customer, product ,cart, cartItem);
            await context.SaveChangesAsync();

            var repository = new CartRepository(context);
            var result = await repository.GetOrCreateCartAsync(customer.Id);

            Assert.NotNull(result.Items.First().Product);
            Assert.Single(result.Items);
            Assert.Equal(result.Id, cart.Id);
        }

        [Fact]
        public async Task GetOrCreateCartAsync_CartDoesNotExists_CreatedCart()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Gabriel",
                Email = "g.bera2710@gmail.com",
                PasswordHash = "122345"
            };

            context.Add(customer);
            await context.SaveChangesAsync();

            var repository = new CartRepository(context);
            var result = await repository.GetOrCreateCartAsync(customer.Id);

            Assert.NotNull(result);
            Assert.Equal(result.CustomerId, customer.Id);
            Assert.Single(context.Carts);
        }
    }
}