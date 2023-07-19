using Contracts.Business.Managers;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;
using eShopOnlineApiHost.Extensions;
using eShopOnlineBusiness.Managers;
using eShopOnlineEFCore.Context;
using eShopOnlineRepositories.Managers;
using eShopOnlineUtilities.AutoMapper;
using eShopOnlineUtilities.AutoMapper.Profiles;
using Microsoft.EntityFrameworkCore;

namespace eShopOnlineApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(eShopOnlineApiRestful.AssemblyReference).Assembly);

            // My DIRegister
            builder.Services.DIRegister_ShopOnlineContext(builder.Configuration);
            builder.Services.DIRegister_Repositories();
            builder.Services.DIRegister_Business();
            builder.Services.DIRegister_AutoMapper();

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