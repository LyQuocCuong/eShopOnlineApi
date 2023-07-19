using Contracts.Business.Managers;
using eShopOnlineBusiness.Entities;

namespace eShopOnlineBusiness.Managers
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _company;
        private readonly Lazy<ICustomerService> _customer;
        private readonly Lazy<IEmployeeService> _employee;
        private readonly Lazy<IProductService> _product;
        private readonly Lazy<IStoreService> _store;

        public ServiceManager(ServiceParams serviceParams)
        {
            _company = new Lazy<ICompanyService>(() => new CompanyService(serviceParams));
            _customer = new Lazy<ICustomerService>(() => new CustomerService(serviceParams));
            _employee = new Lazy<IEmployeeService>(() => new EmployeeService(serviceParams));
            _product = new Lazy<IProductService>(() => new ProductService(serviceParams));
            _store = new Lazy<IStoreService>(() => new StoreService(serviceParams));
        }

        public ICompanyService Company => _company.Value;

        public ICustomerService Customer => _customer.Value;

        public IEmployeeService Employee => _employee.Value;

        public IProductService Product => _product.Value;

        public IStoreService Store => _store.Value;
    }
}
