using Contracts.Business.Services;

namespace Contracts.Business.Managers
{
    public interface IServiceManager
    {
        ICompanyService Company { get; }
        
        ICustomerService Customer { get; }

        IEmployeeService Employee { get; }

        IProductService Product { get; }

        IStoreService Store { get; }
    }
}
