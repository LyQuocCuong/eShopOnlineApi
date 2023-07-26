﻿using Contracts.Repositories.Managers;
using eShopOnlineRepositories.Entities;

namespace eShopOnlineRepositories.Managers
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly ShopOnlineContext _context;
        private readonly ILogService _logService;

        private readonly Lazy<ICompanyRepository> _company;
        private readonly Lazy<ICustomerRepository> _customer;
        private readonly Lazy<IEmployeeRepository> _employee;
        private readonly Lazy<IProductRepository> _product;
        private readonly Lazy<IStoreRepository> _store;

        public RepositoryManager(RepositoryParams repositoryParams) 
        {
            _context = repositoryParams.Context;
            _logService = repositoryParams.LogService;

            _company = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryParams));
            _customer = new Lazy<ICustomerRepository>(() => new CustomerRepository(repositoryParams));
            _employee = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryParams));
            _product = new Lazy<IProductRepository>(() => new ProductRepository(repositoryParams));
            _store = new Lazy<IStoreRepository>(() => new StoreRepository(repositoryParams));
        }

        public void SaveChanges()
        {
            _logService.LogInfo(EFCoreLogMessages.BeginSaving);
            _context.SaveChanges();
            _logService.LogInfo(EFCoreLogMessages.EndSaving);
        }

        public ICompanyRepository Company => _company.Value;

        public ICustomerRepository Customer => _customer.Value;

        public IEmployeeRepository Employee => _employee.Value;

        public IProductRepository Product => _product.Value;

        public IStoreRepository Store => _store.Value;
    }
}
