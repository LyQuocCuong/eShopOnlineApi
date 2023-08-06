using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Parameters
{
    public sealed class ServiceParams
    {
        public readonly IRepositoryManager RepositoryManager;
        public readonly IMapService MapService;

        public ServiceParams(IRepositoryManager repositoryManager, 
                             IMapService mapService)
        {
            RepositoryManager = repositoryManager;
            MapService = mapService;
        }
    }
}
