
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Polly;

namespace Ecommerce.Api.Search
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ConfigureServices(builder.Services,builder.Configuration);
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService,CustomerService>();

            services.AddHttpClient("OrdersService", client =>
            {
                client.BaseAddress = new Uri(configuration["Services:orders"]); 
            });

            services.AddHttpClient("ProductsService", client =>
            {
                client.BaseAddress = new Uri(configuration["Services:Products"]);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

            services.AddHttpClient("CustomersService", client =>
            {
                client.BaseAddress = new Uri(configuration["Services:Customers"]);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _=> TimeSpan.FromMilliseconds(500)));
        }
    }
}