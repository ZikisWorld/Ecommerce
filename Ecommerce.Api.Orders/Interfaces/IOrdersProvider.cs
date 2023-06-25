namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
