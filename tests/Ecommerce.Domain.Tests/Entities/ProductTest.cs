using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Tests.Entities
{
    public class ProductTest
    {
       [Fact]
       public void EnsureStockAvailable_QuantityExceedsStock_ThrowsException()
        {
            var product = new Product
            {
                Name = "Tenis",
                Description = "Nike",
                Quantity = 10
            };

            var requestedQuantity = 11;

            Assert.Throws<DomainException>(() => product.EnsureStockAvailable(requestedQuantity));
        }

        [Fact]
        public void EnsureStockAvailable_QuantityWithinStock_DoesNotThrow()
        {
            var product = new Product
            {
                Name = "Tenis",
                Description = "Nike",
                Quantity = 10
            };

            var quantityValid = 10;

            var exception = Record.Exception(() => product.EnsureStockAvailable(quantityValid));

            Assert.Null(exception);
        }
    }
}