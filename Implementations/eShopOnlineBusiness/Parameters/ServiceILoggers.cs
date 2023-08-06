using eShopOnlineBusiness.Entities;

namespace eShopOnlineBusiness.Parameters
{
    public sealed class ServiceILoggers
    {
        public readonly ILogger<CompanyService> Company;
        public readonly ILogger<CustomerService> Customer;
        public readonly ILogger<EmployeeService> Employee;
        public readonly ILogger<ProductService> Product;
        public readonly ILogger<StoreService> Store;

        public ServiceILoggers(ILogger<CompanyService> company,
                            ILogger<CustomerService> customer,
                            ILogger<EmployeeService> employee,
                            ILogger<ProductService> product,
                            ILogger<StoreService> store)
        {
            Company = company;
            Customer = customer;
            Employee = employee;
            Product = product;
            Store = store;
        }
    }
}
