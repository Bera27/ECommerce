using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Tests.Entities
{
    public class OrderItemTest
    {
        [Theory]
        [InlineData(90.0)]
        [InlineData(0.8)]
        public void ApplyDiscount_ValueOutsideValidRange_ThrowsException(decimal discount)
        {
            var orderItem = new OrderItem
            {
                NameProduct = "Tenis"
            };

            Assert.Throws<DomainException>(() => orderItem.ApplyDiscount(discount));
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(50.0)]
        [InlineData(80.0)]
        public void ApplyDiscount_DiscountValid_DoesNotThrow(decimal discount)
        {
            var orderItem = new OrderItem
            {
                NameProduct = "Tenis"
            };

            var exception = Record.Exception(() => orderItem.ApplyDiscount(discount));
            Assert.Null(exception);
        }
    }
}