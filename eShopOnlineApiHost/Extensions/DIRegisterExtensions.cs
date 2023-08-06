﻿using Contracts.Business.Managers;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;
using eShopOnlineApiRestful.Parameters;
using eShopOnlineBusiness.Managers;
using eShopOnlineBusiness.Parameters;
using eShopOnlineEFCore.Context;
using eShopOnlineRepositories.Managers;
using eShopOnlineRepositories.Parameters;
using eShopOnlineUtilities.AutoMapper;
using eShopOnlineUtilities.AutoMapper.Profiles;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace eShopOnlineApiHost.Extensions
{
    public static class DIRegisterExtensions
    {
        public static void DIRegister_eShopOnlineEFCore(this IServiceCollection services,
                                                             IConfiguration configuration)
        {
            DIRegister_ShopOnlineContext(services, configuration);
        }

        public static void DIRegister_eShopOnlineUtilities(this IServiceCollection services,
                                                                WebApplicationBuilder builder)
        {
            DIRegister_NLog(builder);
            DIRegister_AutoMapper(services);
        }

        public static void DIRegister_eShopOnlineRepositories(this IServiceCollection services)
        {
            services.AddSingleton<RepositoryILoggers>();
            services.AddScoped<RepositoryParams>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void DIRegister_eShopOnlineBusiness(this IServiceCollection services)
        {
            services.AddSingleton<ServiceILoggers>();
            services.AddScoped<ServiceParams>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void DIRegister_eShopOnlineApiRestful(this IServiceCollection services)
        {
            services.AddScoped<ControllerParams>();
        }

        #region SUB METHODS

        private static void DIRegister_ShopOnlineContext(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionStrings = configuration.GetConnectionString(SystemVariables.EShopConnectionString);
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

        private static void DIRegister_NLog(WebApplicationBuilder builder)
        {
            // NLog: Dependency injection ==> ILogger<T> interface
            builder.Logging.ClearProviders();   // Clear ALL default LogProviders
            builder.Host.UseNLog();
        }

        private static void DIRegister_AutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IMapService, AutoMapperService>();
        }

        #endregion
    }
}
