using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            //await Task.Delay(10);
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);
            if(orderResult.IsSuccess)
            {
                foreach(var order in orderResult.orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ? productResult.Products.Where(x => x.Id == item.ProductId).FirstOrDefault()?.Name : "Product Name not available";
                    }
                    order.CustomerName = customerResult.IsSuccess ? customerResult.customer?.Name : "Custmer Name not available";
                }
                var result = new { Orders = orderResult.orders };
                return (true, result);
            }
            return (false, null);
        }
    }
}
