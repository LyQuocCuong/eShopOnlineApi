using eShopOnlineEFCore.Extensions;

namespace eShopOnlineEFCore.Context
{
    public sealed class ShopOnlineContext : DbContext
    {
        public ShopOnlineContext(DbContextOptions<ShopOnlineContext> options)
            : base (options)
        {
            RelationalDatabaseFacadeExtensions.Migrate(this.Database);  // Auto create Database if NOT existing
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllEntitiesConfiguration();
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Store> Stores { get; set; }
    }
}
