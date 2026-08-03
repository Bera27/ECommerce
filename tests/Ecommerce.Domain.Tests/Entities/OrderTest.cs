using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Tests.Entities
{
    public class OrderTest
    {
        [Theory]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.InPreparation)]
        [InlineData(OrderStatus.Shipped)]
        public void UpdateStatus_VerifyTransitionFlowAwaitingPayment_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.AwaitingPayment
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }

        [Theory]
        [InlineData(OrderStatus.Paid)]
        [InlineData(OrderStatus.Cancelled)]
        public void UpdateStatus_ValidTransitionsFromAwaitingPayment_DoesNotThrow(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.AwaitingPayment
            };

            var exception = Record.Exception(() => order.UpdateStatus(status));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(OrderStatus.AwaitingPayment)]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.Shipped)]
        [InlineData(OrderStatus.Returned)]
        public void UpdateStatus_VerifyTransitionFlowPaid_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Paid
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }

        [Theory]
        [InlineData(OrderStatus.InPreparation)]
        [InlineData(OrderStatus.Cancelled)]
        public void UpdateStatus_ValidTransitionsFromPaid_DoesNotThrow(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Paid
            };

            var exception = Record.Exception(() => order.UpdateStatus(status));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(OrderStatus.AwaitingPayment)]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.Paid)]
        [InlineData(OrderStatus.Returned)]
        public void UpdateStatus_VerifyTransitionFlowInPreparation_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.InPreparation
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }

        [Theory]
        [InlineData(OrderStatus.Shipped)]
        [InlineData(OrderStatus.Cancelled)]
        public void UpdateStatus_ValidTransitionsFromInPreparation_DoesNotThrow(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.InPreparation
            };

            var exception = Record.Exception(() => order.UpdateStatus(status));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(OrderStatus.AwaitingPayment)]
        [InlineData(OrderStatus.Cancelled)]
        [InlineData(OrderStatus.Paid)]
        [InlineData(OrderStatus.Returned)]
        [InlineData(OrderStatus.InPreparation)]
        public void UpdateStatus_VerifyTransitionFlowShipped_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Shipped
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }

        [Theory]
        [InlineData(OrderStatus.Delivered)]
        public void UpdateStatus_ValidTransitionsFromShipped_DoesNotThrow(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Shipped
            };

            var exception = Record.Exception(() => order.UpdateStatus(status));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(OrderStatus.AwaitingPayment)]
        [InlineData(OrderStatus.Cancelled)]
        [InlineData(OrderStatus.Paid)]
        [InlineData(OrderStatus.Shipped)]
        [InlineData(OrderStatus.InPreparation)]
        public void UpdateStatus_VerifyTransitionFlowDelivered_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Delivered
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }

        [Theory]
        [InlineData(OrderStatus.Returned)]
        public void UpdateStatus_ValidTransitionsFromDelivered_DoesNotThrow(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Delivered
            };

            var exception = Record.Exception(() => order.UpdateStatus(status));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(OrderStatus.AwaitingPayment)]
        [InlineData(OrderStatus.InPreparation)]
        [InlineData(OrderStatus.Shipped)]
        public void UpdateStatus_VerifyTransitionFlowCancelled_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Cancelled
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }

        [Theory]
        [InlineData(OrderStatus.Delivered)]
        public void UpdateStatus_VerifyTransitionFlowReturned_ThrowsException(OrderStatus status)
        {
            var order = new Order
            {
                City = "SP",
                Street = "Rua",
                Neighborhood = "Bairro",
                Complement = "casa 5",
                Number = "700",
                ZipCode = "10101101",
                Status = OrderStatus.Returned
            };

            Assert.Throws<DomainException>(() => order.UpdateStatus(status));
        }
    }
}