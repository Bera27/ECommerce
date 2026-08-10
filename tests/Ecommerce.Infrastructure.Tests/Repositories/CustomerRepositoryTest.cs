using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Tests.Repositories
{
    public class CustomerRepositoryTest
    {
        [Fact]
        public async Task GetByEmailAsync_CustomerExists_ReturnsCustomer()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var customer = new Customer
            {
                Name = "Gabriel",
                Email = "g.bera2710@gmail.com",
                PasswordHash = "122345"
            };

            context.Add(customer);
            await context.SaveChangesAsync();

            var repository = new CustomerRepository(context);
            var result = await repository.GetByEmailAsync(customer.Email);

            Assert.NotNull(result);
            Assert.Equal("g.bera2710@gmail.com", result.Email);
        }

        [Fact]
        public async Task RemoveCustomerAsync_CustomerExists_ReturnTrue()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var customer = new Customer
            {
                Name = "Gabriel",
                Email = "g.bera2710@gmail.com",
                PasswordHash = "122345"
            };

            context.Add(customer);
            await context.SaveChangesAsync();

            var repository = new CustomerRepository(context);
            var result = await repository.RemoveCustomerAsync(customer.Id);

            Assert.True(result);
            Assert.Null(await context.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id));
        }

        [Fact]
        public async Task RemoveCustomerAsync_CustomerDoesNotExist_ReturnFalse()
        {
             var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var repository = new CustomerRepository(context);
            var result = await repository.RemoveCustomerAsync(Guid.NewGuid());

            Assert.False(result);
        }
    }
}