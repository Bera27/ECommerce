using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.Services;
using Ecommerce.Application.Requests;
using Ecommerce.Application.UseCases.Customers;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Moq;

namespace Ecommerce.Application.Tests.UseCases.Customers
{
    public class LoginTest
    {
        [Fact]
        public async Task ExecuteAsync_EmailDoesNotExists_ThrowsDomainException()
        {
            var mockCustomer = new Mock<ICustomerRepository>();
            var mockPassHash = new Mock<IPasswordHasher>();
            var mockToken = new Mock<ITokenService>();

            var loginRequest = new LoginRequest
            {
                Email = "g.bera@gmai.com",
                Password = "12314214"
            };

            mockCustomer.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Customer?)null);

            var sut = new Login(mockCustomer.Object, mockPassHash.Object, mockToken.Object);

            await Assert.ThrowsAsync<DomainException>(async () => await sut.ExecuteAsync(loginRequest));
        }

        [Fact]
        public async Task ExecuteAsync_PasswordIncorrect_ThrowsDomainException()
        {
            var mockCustomer = new Mock<ICustomerRepository>();
            var mockPassHash = new Mock<IPasswordHasher>();
            var mockToken = new Mock<ITokenService>();

            var loginRequest = new LoginRequest
            {
                Email = "g.bera@gmai.com",
                Password = "12314214"
            };

            var customer = new Customer
            {
                Name = "gabriel",
                Email = "g.bera@gmail.com",
                PasswordHash = "1212341"
            };
            
            mockCustomer.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(customer);

            mockPassHash.Setup(x => x.ComparePasswordHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var sut = new Login(mockCustomer.Object, mockPassHash.Object, mockToken.Object);

            await Assert.ThrowsAsync<DomainException>(async () => await sut.ExecuteAsync(loginRequest));
        }

        [Fact]
        public async Task ExecuteAsync_EmailAndPasswordCorrect_ReturnsLoginResponse()
        {
            var mockCustomer = new Mock<ICustomerRepository>();
            var mockPassHash = new Mock<IPasswordHasher>();
            var mockToken = new Mock<ITokenService>();

            var loginRequest = new LoginRequest
            {
                Email = "g.bera@gmai.com",
                Password = "12314214"
            };

            var customer = new Customer
            {
                Name = "gabriel",
                Email = "g.bera@gmail.com",
                PasswordHash = "1212341"
            };

            mockCustomer.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(customer);

            mockPassHash.Setup(x => x.ComparePasswordHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            mockToken.Setup(x => x.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns("fake-token-123");

            var sut = new Login(mockCustomer.Object, mockPassHash.Object, mockToken.Object);
            var result = await sut.ExecuteAsync(loginRequest);

            Assert.Equal("fake-token-123", result.Token);
            Assert.Equal(customer.Name, result.Name);
            Assert.Equal(customer.Email, result.Email);
        }
    }
}