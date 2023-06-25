using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Collections;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<IOrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<IOrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(IEnumerable<Order> orders, bool IsSuccess, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("OrdersService");
                var response = await httpClient.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(content,jsonOptions);
                    return (orders, true, null);
                }
                return (null, false, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return (null, false, ex.Message);
            }           
        }
    }
}
