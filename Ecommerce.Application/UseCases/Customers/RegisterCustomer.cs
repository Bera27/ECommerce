using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.Services;
using Ecommerce.Application.Requests;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Application.UseCases.Customers
{
    public class RegisterCustomer
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passHash;
        public RegisterCustomer(ICustomerRepository customerRepository, IPasswordHasher passHash)
        {
            _customerRepository = customerRepository;
            _passHash = passHash;
        }

        public async Task<RegisterCustomerResponse> ExecuteAsync(RegisterCustomerRequest customerRequest)
        {
            var email = await _customerRepository.GetByEmailAsync(customerRequest.Email);

            if(email != null)
                throw new DomainException("Já existe uma conta com este e-mail.");

            var passwordHash = _passHash.HashPassword(customerRequest.Password); 

            var newCustomer = new Customer
            {
                Name = customerRequest.Name,
                Email = customerRequest.Email,
                PasswordHash = passwordHash
            };

            var newAddress = new Address
            {
                State = customerRequest.State,
                City = customerRequest.City,
                Street = customerRequest.Street,
                Neighborhood = customerRequest.Neighborhood,
                Complement = customerRequest.Complement,
                Number = customerRequest.Number,
                ZipCode = customerRequest.ZipCode
            };

            newCustomer.Addresses.Add(newAddress);

            await _customerRepository.CreateCustomerAsync(newCustomer);

            return new RegisterCustomerResponse
            {
                Name = newCustomer.Name,
                Email = newCustomer.Email
            };
        }
    }
}