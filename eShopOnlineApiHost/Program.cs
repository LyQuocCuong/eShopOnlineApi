using Contracts.Utilities.Mapper;
using eShopOnlineEFCore.Context;
using eShopOnlineUtilities.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace eShopOnlineApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ShopOnlineContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("eShopOnlineConnection"))
            );

            builder.Services.AddControllers();
            builder.Services.AddScoped<IMapperService, AutoMapperService>();
            
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}