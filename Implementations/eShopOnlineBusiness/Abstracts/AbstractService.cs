using Contracts.Business.Abstracts;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Abstracts
{
    public abstract class AbstractService<TDerivedService> : IAbstractService
    {
        protected readonly ILogger<TDerivedService> _logger;
        protected readonly IRepositoryManager _repository;
        protected readonly IMapService _mapService;

        protected AbstractService(ILogger<TDerivedService> logger, 
                                  ServiceParams serviceParams)
        {
            _logger = logger;
            _repository = serviceParams.RepositoryManager;
            _mapService = serviceParams.MapService;
        }
    }
}
