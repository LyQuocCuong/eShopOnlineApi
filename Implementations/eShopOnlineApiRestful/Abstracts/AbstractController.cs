using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Abstracts
{
    public abstract class AbstractController : ControllerBase
    {
        protected readonly IServiceManager _services;

        protected AbstractController(ControllerParams controllerParams)
        {
            _services = controllerParams.Service;
        }
    }
}
