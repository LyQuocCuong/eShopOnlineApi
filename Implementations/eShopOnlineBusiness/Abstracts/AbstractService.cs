using Contracts.Business.Abstracts;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;

namespace eShopOnlineBusiness.Abstracts
{
    public abstract class AbstractService : IAbstractService
    {
        private readonly ILogService _logService;
        protected readonly IRepositoryManager _repository;
        protected readonly IMapService _mapService;
        protected abstract string ChildClassName { get; }

        protected AbstractService(ServiceParams serviceParams)
        {
            _logService = serviceParams.LogService;
            _repository = serviceParams.RepositoryManager;
            _mapService = serviceParams.MapService;
        }

        private string GenerateMessages(string methodName, string message)
        {
            return LogMessages.FormatMessageForBusiness(ChildClassName, methodName, message);
        }

        public void LogDebug(string methodName, string message)
        {
            _logService.LogDebug(GenerateMessages(methodName, message));
        }

        public void LogError(string methodName, string message)
        {
            _logService.LogError(GenerateMessages(methodName, message));
        }

        public void LogInfo(string methodName, string message)
        {
            _logService.LogInfo(GenerateMessages(methodName, message));
        }

        public void LogWarning(string methodName, string message)
        {
            _logService.LogWarning(GenerateMessages(methodName, message));
        }

    }
}
