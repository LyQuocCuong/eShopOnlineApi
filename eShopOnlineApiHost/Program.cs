using NLog;

namespace eShopOnlineApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = LogManager.Setup()
                            .LoadConfigurationFromFile(
                                optional: true,
                                candidateFilePaths: new List<string> { SystemVariables.NLogConfigFilePath })
                            .GetCurrentClassLogger();
            logger.Info("INIT MAIN");

            try
            {
                var builder = WebApplication.CreateBuilder(args);   // Include 4 default LogProviders

                // Add the ApiRESTful module
                builder.Services.AddControllers()
                    .AddApplicationPart(typeof(eShopOnlineApiRestful.AssemblyReference).Assembly);

                // DI Registration (AFTER adding Controllers)
                builder.Services.DIRegister_eShopOnlineEFCore(builder.Configuration);
                builder.Services.DIRegister_eShopOnlineUtilities(builder);
                builder.Services.DIRegister_eShopOnlineRepositories();
                builder.Services.DIRegister_eShopOnlineBusiness();
                builder.Services.DIRegister_eShopOnlineApiRestful();

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
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}