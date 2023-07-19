using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Abstracts
{
    [ApiController]
    [Route("api")]
    public abstract class AbstractApiController : ControllerBase
    {
        protected readonly IServiceManager _services;

        protected AbstractApiController(ControllerParams controllerParams)
        {
            _services = controllerParams.Service;
        }
    }
}
