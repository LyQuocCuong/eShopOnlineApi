using Contracts.Business.Abstracts;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Abstracts
{
    public abstract class AbstractService : IAbstractService
    {
        protected readonly IRepositoryManager _repositoryManager;
        protected readonly IMapperService _mapperService;

        protected AbstractService(ServiceParams serviceParams)
        {
            _repositoryManager = serviceParams.RepositoryManager;
            _mapperService = serviceParams.MapperService;
        }

    }
}
