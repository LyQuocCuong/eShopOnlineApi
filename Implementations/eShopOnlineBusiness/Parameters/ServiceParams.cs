using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Parameters
{
    public sealed class ServiceParams
    {
        public readonly IRepositoryManager RepositoryManager;
        public readonly IMapService MapService;
        public readonly ILogService LogService;

        public ServiceParams(IRepositoryManager repositoryManager, 
                             IMapService mapService,
                             ILogService logService)
        {
            RepositoryManager = repositoryManager;
            MapService = mapService;
            LogService = logService;
        }
    }
}
