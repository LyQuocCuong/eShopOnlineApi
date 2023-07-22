using Contracts.Repositories.Managers;
using Contracts.Utilities.Logger;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Parameters
{
    public sealed class ServiceParams
    {
        public readonly IRepositoryManager RepositoryManager;
        public readonly IMapperService MapperService;
        public readonly ILogService LogService;

        public ServiceParams(IRepositoryManager repositoryManager, 
                             IMapperService mapperService,
                             ILogService logService)
        {
            RepositoryManager = repositoryManager;
            MapperService = mapperService;
            LogService = logService;
        }
    }
}
