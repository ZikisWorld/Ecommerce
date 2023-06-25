using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomersController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync() 
        { 
            var result = await customerProvider.GetCustomersAsync();
            if(result.IsSuccess)
                return Ok(result.customers);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customerProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
                return Ok(result.customer);
            return NotFound();
        }

    }
}
