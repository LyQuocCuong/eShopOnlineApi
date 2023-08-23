using eShopOnlineBusiness.Parameters;
using Microsoft.Extensions.Logging;
using MockContracts.Repositories.Managers;
using MockContracts.Utilities.LogService;
using MockContracts.Utilities.MapService;

namespace UnitTest.eShopOnlineBusiness.Abstracts
{
    public abstract class AbstractServiceTest<TService>
    {
        protected abstract TService InitService();

        protected ILogger<TService> GetILogger()
        {
            return MockILogger<TService>.GetInstance().Object;
        }

        protected ServiceParams GetServiceParams()
        {
            return new ServiceParams(
                MockIRepositoryManager.GetInstance().Object,
                FakeAutoMapperService.GetInstance()
            );
        }

    }
}
