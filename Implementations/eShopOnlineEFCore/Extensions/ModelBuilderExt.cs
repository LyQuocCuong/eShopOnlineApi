using eShopOnlineEFCore.Configurations.Entities;

namespace eShopOnlineEFCore.Extensions
{
    internal static class ModelBuilderExt
    {
        public static void ApplyAllEntitiesConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Company>(new CompanyConfig());
            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfig());
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfig());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfig());
            modelBuilder.ApplyConfiguration<Store>(new StoreConfig());
        }
    }
}
