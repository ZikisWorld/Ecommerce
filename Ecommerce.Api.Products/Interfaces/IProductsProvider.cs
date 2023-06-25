using Ecommerce.Api.Products.Models;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product product, string ErrorMessage)> GetProductAsync(int id);
    }
}
