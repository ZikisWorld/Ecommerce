using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerDbContext> logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerDbContext> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id =1 , Name = "Zikriya Shaik", Address = "Guntur" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Moulali Shaik", Address = "Narasaraopet" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Sai Korumilli", Address = "Hyderabad" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying Customers");
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
        public  async Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }            
        }

       
    }
}
