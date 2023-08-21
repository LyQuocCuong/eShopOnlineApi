using Contracts.Repositories.Entities;

namespace Contracts.Repositories.Managers
{
    public interface IRepositoryManager
    {
        Task SaveChangesAsync();

        ICompanyRepository Company { get; }

        ICustomerRepository Customer { get; }

        IEmployeeRepository Employee { get; }

        IProductRepository Product { get; }

        IStoreRepository Store { get; }
    }
}
