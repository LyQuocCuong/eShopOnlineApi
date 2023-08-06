using eShopOnlineRepositories.Entities;

namespace eShopOnlineRepositories.Parameters
{
    public sealed class RepositoryILoggers
    {
        public readonly ILogger<CompanyRepository> Company;
        public readonly ILogger<CustomerRepository> Customer;
        public readonly ILogger<EmployeeRepository> Employee;
        public readonly ILogger<ProductRepository> Product;
        public readonly ILogger<StoreRepository> Store;

        public RepositoryILoggers(ILogger<CompanyRepository> company, 
                            ILogger<CustomerRepository> customer, 
                            ILogger<EmployeeRepository> employee, 
                            ILogger<ProductRepository> product, 
                            ILogger<StoreRepository> store)
        {
            Company = company;
            Customer = customer;
            Employee = employee;
            Product = product;
            Store = store;
        }
    }
}
