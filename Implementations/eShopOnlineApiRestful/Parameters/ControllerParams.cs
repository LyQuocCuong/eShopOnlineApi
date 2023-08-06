using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Parameters
{
    public sealed class ControllerParams
    {
        public readonly IServiceManager ServiceManager;

        public ControllerParams(IServiceManager serviceManager) 
        {
            ServiceManager = serviceManager;
        }
    }
}
