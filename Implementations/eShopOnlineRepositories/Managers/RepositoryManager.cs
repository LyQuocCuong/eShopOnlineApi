using Contracts.Repositories.Managers;
using eShopOnlineRepositories.Entities;

namespace eShopOnlineRepositories.Managers
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly ShopOnlineContext _context;

        private readonly Lazy<ICompanyRepository> _company;
        private readonly Lazy<ICustomerRepository> _customer;
        private readonly Lazy<IEmployeeRepository> _employee;
        private readonly Lazy<IProductRepository> _product;
        private readonly Lazy<IStoreRepository> _store;

        public RepositoryManager(RepositoryParams repositoryParams) 
        {
            _context = repositoryParams.Context;

            _company = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryParams));
            _customer = new Lazy<ICustomerRepository>(() => new CustomerRepository(repositoryParams));
            _employee = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryParams));
            _product = new Lazy<IProductRepository>(() => new ProductRepository(repositoryParams));
            _store = new Lazy<IStoreRepository>(() => new StoreRepository(repositoryParams));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ICompanyRepository Company => _company.Value;

        public ICustomerRepository Customer => _customer.Value;

        public IEmployeeRepository Employee => _employee.Value;

        public IProductRepository Product => _product.Value;

        public IStoreRepository Store => _store.Value;
    }
}
