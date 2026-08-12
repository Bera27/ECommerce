using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Tests.Repositories
{
    public class OrderRepositoryTest
    {
        [Fact]
        public async Task GetAllAsync_CustomerExists_ReturnAllOrders()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var customerA = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Gabriel",
                Email = "g.bera2710@gmail.com",
                PasswordHash = "122345"
            };

            var customerB = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Bruno",
                Email = "Bruno.bera@gmail.com",
                PasswordHash = "122345"
            };

            var customerAOrder1 = new Order
            {
                CustomerId = customerA.Id,
                OrderDate = DateTime.UtcNow.AddDays(-1),
                City = "Sorocaba",
                Street = "Rua das Flores",
                Neighborhood = "Centro",
                Complement = "Apto 101",
                Number = "123", 
                ZipCode = "18010-000"
            };

            var customerAOrder2 = new Order
            {
                CustomerId = customerA.Id,
                City = "Sorocaba",
                Street = "Rua das ruas",
                Neighborhood = "leste",
                Complement = "Apto 141",
                Number = "34", 
                ZipCode = "1140-000"
            };

            var customerBOrder3 = new Order
            {
                CustomerId = customerB.Id,
                City = "Sorocaba",
                Street = "Rua das logo ali",
                Neighborhood = "norte",
                Complement = "Apto 1",
                Number = "4", 
                ZipCode = "1560-000"
            };

            await context.AddRangeAsync(customerA, customerB, customerAOrder1, customerAOrder2, customerBOrder3);
            await context.SaveChangesAsync();

            var repository = new OrderRepository(context);
            var result = await repository.GetAllAsync(customerA.Id);

            Assert.NotNull(result);
            Assert.Equal(result.First().Id, customerAOrder1.Id);
            Assert.Equal(2, result.Count());
            Assert.All(result, order =>
            {
                Assert.Equal(customerA.Id, order.CustomerId);
            });
        }

        [Fact]
        public async Task GetByIdAsync_OrderExists_ReturnOrder()
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

            var order = new Order
            {
                CustomerId = customer.Id,
                City = "Sorocaba",
                Street = "Rua das logo ali",
                Neighborhood = "norte",
                Complement = "Apto 1",
                Number = "4", 
                ZipCode = "1560-000"
            };

            await context.AddRangeAsync(customer, order);
            await context.SaveChangesAsync();

            var repository = new OrderRepository(context);
            var result = await repository.GetByIdAsync(order.Id);
            
            Assert.NotNull(result);
            Assert.Equal(result.Id, order.Id);
        }
    }
}