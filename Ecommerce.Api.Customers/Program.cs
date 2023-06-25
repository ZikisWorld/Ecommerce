
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<ICustomerProvider,CustomerProvider>();
            builder.Services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseInMemoryDatabase("Customers");
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
    }
}