using Contracts.Repositories.Managers;
using eShopOnlineRepositories.Entities;

namespace eShopOnlineRepositories.Managers
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly ShopOnlineContext _context;

        public RepositoryManager(ShopOnlineContext context) 
        {
            _context = context;
        }

        public ICompanyRepository Company => new CompanyRepository(_context);

        public ICustomerRepository Customer => new CustomerRepository(_context);

        public IEmployeeRepository Employee => new EmployeeRepository(_context);

        public IProductRepository Product => new ProductRepository(_context);

        public IStoreRepository Store => new StoreRepository(_context);
    }
}
