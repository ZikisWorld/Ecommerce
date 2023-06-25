using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(Customer customer, bool IsSuccess, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("CustomersService");
                var response = await httpClient.GetAsync($"api/Customers/{customerId}");
                if (response.IsSuccessStatusCode) 
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var jsonOptions = new JsonSerializerOptions() {  PropertyNameCaseInsensitive= true };
                    var result = JsonSerializer.Deserialize<Customer>(content,jsonOptions);
                    return (result, true, null);
                }
                return (null, false, "Customer Not found");
            }
            catch ( Exception ex)
            {
                logger.LogError(ex.Message);
                return (null, false, ex.Message);
            }            
        }
    }
}
