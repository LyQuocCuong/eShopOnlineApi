using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Parameters
{
    public sealed class ControllerParams
    {
        public readonly IServiceManager ServiceManager;
        public readonly ILogService LogService;

        public ControllerParams(IServiceManager serviceManager,
                                ILogService logService) 
        {
            ServiceManager = serviceManager;
            LogService = logService;
        }
    }
}
