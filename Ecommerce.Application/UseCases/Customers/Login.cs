using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.Services;
using Ecommerce.Application.Requests;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Application.UseCases.Customers
{
    public class Login
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passHash;
        private readonly ITokenService _tokenService;

        public Login(ICustomerRepository customerRepository, IPasswordHasher passHash, ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _passHash = passHash;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest loginRequest)
        {
            var customer = await _customerRepository.GetByEmailAsync(loginRequest.Email);

            if(customer == null || !_passHash.ComparePasswordHash(customer.PasswordHash, loginRequest.Password))
                throw new DomainException("Email ou senha incorreto ou inexistente.");

            var token = _tokenService.GenerateToken(customer.Id, customer.Email);

            return new LoginResponse
            {
                Name = customer.Name,
                Email = customer.Email,
                Token = token
            };
        }
    }
}