using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(Customer customer, bool IsSuccess, string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
