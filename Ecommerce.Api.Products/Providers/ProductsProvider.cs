using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext,ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Inventory = 100, Price = 20 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Inventory = 1000, Price = 5 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Inventory = 200, Price = 150 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Inventory = 2000, Price = 200 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products,
            string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                  var result =  mapper.Map< IEnumerable< Db.Product>,IEnumerable< Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

       public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if(product != null )
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, null);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false,null,ex.Message);
            }
        }
    }
}
