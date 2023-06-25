using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(IEnumerable<Order> orders, bool IsSuccess, string ErrorMessage )> GetOrdersAsync(int customerId);
    }
}
