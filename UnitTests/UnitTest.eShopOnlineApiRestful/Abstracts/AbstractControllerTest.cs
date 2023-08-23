using eShopOnlineApiRestful.Parameters;
using Microsoft.Extensions.Logging;
using MockContracts.Business.Managers;
using MockContracts.Utilities.LogService;

namespace UnitTest.eShopOnlineApiRestful.Abstracts
{
    public abstract class AbstractControllerTest<TController>
    {
        protected abstract TController InitController();

        protected ILogger<TController> GetILogger()
        {
            return MockILogger<TController>.GetInstance().Object;
        }

        protected ControllerParams GetControllerParams()
        {
            return new ControllerParams(MockIServiceManager.GetInstance().Object);
        }
    }
}
