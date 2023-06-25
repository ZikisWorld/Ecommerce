
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IProductsProvider, ProductsProvider>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products");
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