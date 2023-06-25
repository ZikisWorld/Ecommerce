using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> Products, bool IsSuccess, string ErrorMessage)> GetProductsAsync();
    }
}
