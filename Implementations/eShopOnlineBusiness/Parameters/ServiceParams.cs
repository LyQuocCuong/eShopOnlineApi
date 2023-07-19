using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Parameters
{
    public sealed class ServiceParams
    {
        public readonly IRepositoryManager RepositoryManager;
        public readonly IMapperService MapperService;

        public ServiceParams(IRepositoryManager repositoryManager, 
                             IMapperService mapperService)
        {
            RepositoryManager = repositoryManager;
            MapperService = mapperService;
        }
    }
}
