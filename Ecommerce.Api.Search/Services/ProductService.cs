using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductService> logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
		{
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(IEnumerable<Product> Products, bool IsSuccess, string ErrorMessage)> GetProductsAsync()
        {
			try
			{
                var client = httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/Products");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content,jsonOptions);
                    return (products, true, null);
                }
                return (null, false, "Not found");
			}
			catch (Exception ex)
			{
                logger.LogError(ex.Message);
                return (null, false, ex.Message);				
			}
        }
    }
}
