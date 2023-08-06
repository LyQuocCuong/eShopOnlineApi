using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Abstracts
{
    [ApiController]
    [Route("api")]
    public abstract class AbstractApiController<TDerivedController> : ControllerBase
    {
        protected readonly ILogger<TDerivedController> _logger;
        protected readonly IServiceManager _services;

        protected AbstractApiController(ILogger<TDerivedController> logger, 
                                        ControllerParams controllerParams)
        {
            _logger = logger;
            _services = controllerParams.ServiceManager;
        }
    }
}
