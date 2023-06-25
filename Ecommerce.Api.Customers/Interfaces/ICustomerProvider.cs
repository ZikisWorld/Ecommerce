using Ecommerce.Api.Customers.Models;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
