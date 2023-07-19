using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Parameters
{
    public sealed class ControllerParams
    {
        public readonly IServiceManager Service;

        public ControllerParams(IServiceManager service) 
        {
            Service = service;
        }
    }
}
