using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Tests.Repositories
{
    public class ProductRepositoryTest
    {
        [Fact]
        public async Task GetByIdAsync_ProductExists_ReturnsProduct()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Notebook Gamer",
                Price = 4599.90m,
                Weight = 2.5m,
                Height = 2.5m,
                Width = 35.5m,
                Length = 25.0m,
                Quantity = 10,
                Description = "Notebook de alta performance para jogos e trabalho pesado.",
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            var result = await repository.GetByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal("Notebook Gamer", result.Name);
        }

        [Fact]
        public async Task GetAllProductsAsync_ActiveAndInactiveProductsExist_ReturnsProductActive()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var productActive = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Notebook LG",
                Description = "16 gb ram",
            };

            var productDeactivate = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Notebook Asus",
                Description = "32 gb ram",
            };

            productDeactivate.Deactivate();

            context.Products.Add(productActive);
            context.Products.Add(productDeactivate);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            var result = await repository.GetAllProductsAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.DoesNotContain(result, p => p.Id == productDeactivate.Id);
            Assert.Contains(result, p => p.Id == productActive.Id);
        }

        [Fact]
        public async Task RemoveProductAsync_ProductExists_ReturnTrue()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Notebook LG",
                Description = "16 gb ram",
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            var result = await repository.RemoveProductAsync(product.Id);

            Assert.True(result);
            Assert.Null(await context.Products.FirstOrDefaultAsync(x => x.Id == product.Id));
        }

        [Fact]
        public async Task RemoveProductAsync_ProductDoesNotExist_ReturnFalse()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new EcommerceDbContext(options);

            var repository = new ProductRepository(context);
            var result = await repository.RemoveProductAsync(Guid.NewGuid());

            Assert.False(result);
        }
    }
}