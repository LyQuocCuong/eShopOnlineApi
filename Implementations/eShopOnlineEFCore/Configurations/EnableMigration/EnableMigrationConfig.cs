using eShopOnlineEFCore.Context;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eShopOnlineEFCore.Configurations.EnableMigration
{
    internal sealed class EnableMigrationConfig : IDesignTimeDbContextFactory<ShopOnlineContext>
    {
        public ShopOnlineContext CreateDbContext(string[] args)
        {
            // Define the [DbContext] was created at the DESIGN-TIME
            // Install 2 packages:
            // 1) Microsoft.Extensions.Configuration.FileExtensions    --> SetBasePath()
            // 2) Microsoft.Extensions.Configuration.Json              --> AddJsonFile()
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../eShopOnlineApiHost"))
                .AddJsonFile("appsettings.Development.json")
                .Build();

            // MigrationsAssembly: where executing Migration commands.
            var builder = new DbContextOptionsBuilder<ShopOnlineContext>()
                            .UseSqlServer(configuration.GetConnectionString("eShopOnlineConnection"),
                                           m => m.MigrationsAssembly("eShopOnlineEFCore"));

            return new ShopOnlineContext(builder.Options);  // that's why need Constructor in [ShopOnlineContext]
        }
    }
}
