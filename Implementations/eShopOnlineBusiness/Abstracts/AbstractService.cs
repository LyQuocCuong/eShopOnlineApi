using Contracts.Business.Abstracts;
using Contracts.Repositories.Managers;
using Contracts.Utilities.Mapper;
using Shared.Templates;

namespace eShopOnlineBusiness.Abstracts
{
    public abstract class AbstractService : IAbstractService
    {
        //private readonly ILogService _logService;
        protected readonly IRepositoryManager _repository;
        protected readonly IMapService _mapService;
        protected abstract string ClassName { get; }

        protected AbstractService(ServiceParams serviceParams)
        {
            //_logService = serviceParams.LogService;
            _repository = serviceParams.RepositoryManager;
            _mapService = serviceParams.MapService;
        }

        #region LOG FUNCTIONS

        protected void LogMethodInfo(string methodName)
        {
            //_logService.LogInfo(LogContentsTemplate.BusinessMethodInfo(this.ClassName, methodName));
        }

        protected void LogMethodReturnInfo(string result)
        {
            //_logService.LogInfo(LogContentsTemplate.BusinessMethodReturn(result));
        }

        private static string FormatContent(string content)
        {
            return LogContentsTemplate.BusinesFormat(content);
        }

        protected void LogInfo(string message)
        {
            //_logService.LogInfo(FormatContent(message));
        }

        protected void LogError(string message)
        {
            //_logService.LogError(FormatContent(message));
        }

        protected void LogDebug(string message)
        {
            //_logService.LogDebug(FormatContent(message));
        }

        protected void LogWarning(string message)
        {
            //_logService.LogWarning(FormatContent(message));
        }

        #endregion
    }
}
