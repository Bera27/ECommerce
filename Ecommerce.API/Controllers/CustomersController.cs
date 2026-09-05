using Ecommerce.Application.Requests;
using Ecommerce.Application.UseCases.Customers;
using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly RegisterCustomer _registerCustomer;
        private readonly ILogger<CustomersController> _logger;
        private readonly Login _login;

        public CustomersController(RegisterCustomer registerCustomer,
               ILogger<CustomersController> logger,
               Login login)
        {
            _registerCustomer = registerCustomer;
            _login = login;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomersAsync([FromBody] RegisterCustomerRequest customerRequest)
        {
            try
            {
                var response = await _registerCustomer.ExecuteAsync(customerRequest);

                return Created($"api/customers/{response.Id}", response);
            }
            catch(DomainException)
            {
                return StatusCode(409, "Já existe uma conta com este Email.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar novo cliente.");

                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLoginAsync([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var request = await _login.ExecuteAsync(loginRequest);

                return Ok(request);
            }
            catch(DomainException)
            {
                return StatusCode(401, "Email ou senha incorreto ou inexistente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer login.");
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}