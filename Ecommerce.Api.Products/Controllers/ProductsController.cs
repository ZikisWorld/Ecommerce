using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider    )
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();
            if(result.IsSuccess)
                return Ok(result.products);
          return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.IsSuccess)
                return Ok(result.product);
            return NotFound();
        }
    }
}
