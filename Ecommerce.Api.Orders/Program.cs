
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IOrdersProvider, OrdersProvider>();
            builder.Services.AddDbContext<OrdersDbContext>(options =>
            {
                options.UseInMemoryDatabase("Orders");
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