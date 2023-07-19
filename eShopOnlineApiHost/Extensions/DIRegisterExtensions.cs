using Contracts.Business.Managers;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;
using eShopOnlineBusiness.Managers;
using eShopOnlineBusiness.Parameters;
using eShopOnlineEFCore.Context;
using eShopOnlineRepositories.Managers;
using eShopOnlineRepositories.Parameters;
using eShopOnlineUtilities.AutoMapper;
using eShopOnlineUtilities.AutoMapper.Profiles;
using Microsoft.EntityFrameworkCore;

namespace eShopOnlineApiHost.Extensions
{
    public static class DIRegisterExtensions
    {
        public static void DIRegister_ShopOnlineContext(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionStrings = configuration.GetConnectionString("eShopOnlineConnection");
            if (connectionStrings != null)
            {
                services.AddDbContext<ShopOnlineContext>(
                    options => options.UseSqlServer(connectionStrings)
                );
            }
            else
            {
                throw new Exception("ConnectionStrings is missing");
            }
        }

        public static void DIRegister_Repositories(this IServiceCollection services)
        {
            services.AddScoped<RepositoryParams>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void DIRegister_Business(this IServiceCollection services)
        {
            services.AddScoped<ServiceParams>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void DIRegister_AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IMapperService, AutoMapperService>();
        }
    }
}
