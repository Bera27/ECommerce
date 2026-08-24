using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.Services;
using Ecommerce.Application.Requests;
using Ecommerce.Application.UseCases.Customers;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Moq;

namespace Ecommerce.Application.Tests.UseCases.Customers
{
    public class RegisterCustomerTest
    {
        [Fact]
        public async Task ExecuteAsync_EmailExists_ThrowDomainException()
        {
            var mockCustomer = new Mock<ICustomerRepository>();
            var mockPassHash = new Mock<IPasswordHasher>();

            var customer = new Customer
            {
                Name = "gabriel",
                Email = "g.bera@gmail.com",
                PasswordHash = "1212341"
            };

            var customerRequest = new RegisterCustomerRequest()
            {
                Name = "João da Silva",
                Email = "joao.silva@email.com",
                Password = "SenhaForte123!",
                City = "Sorocaba",
                Street = "Rua das Flores",
                Neighborhood = "Centro",
                Number = "123",
                ZipCode = "18010-000"
            };

            mockCustomer.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(customer);

            var sut = new RegisterCustomer(mockCustomer.Object, mockPassHash.Object);

            await Assert.ThrowsAsync<DomainException>(async () => await sut.ExecuteAsync(customerRequest));
        }

        [Fact]
        public async Task ExecuteAsync_EmailDoesNotExists_ReturnCustomer()
        {
            var mockCustomer = new Mock<ICustomerRepository>();
            var mockPassHash = new Mock<IPasswordHasher>();

            var customerRequest = new RegisterCustomerRequest()
            {
                Name = "João da Silva",
                Email = "joao.silva@email.com",
                Password = "SenhaForte123!",
                City = "Sorocaba",
                Street = "Rua das Flores",
                Neighborhood = "Centro",
                Number = "123",
                ZipCode = "18010-000"
            };

            mockCustomer.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Customer?)null);

            mockPassHash.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("hash-fake");

            var sut = new RegisterCustomer(mockCustomer.Object, mockPassHash.Object);

            var result = await sut.ExecuteAsync(customerRequest);

            Assert.Equal(customerRequest.Name, result.Name);
            Assert.Equal(customerRequest.Email, result.Email);

            mockCustomer.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once);
        }
    }
}