using Contracts.Business.Managers;
using Shared.Templates;

namespace eShopOnlineApiRestful.Abstracts
{
    [ApiController]
    [Route("api")]
    public abstract class AbstractApiController<TDerivedController> : ControllerBase
    {
        protected readonly ILogger<TDerivedController> _logger;

        //private readonly ILogService _logService;
        protected readonly IServiceManager _services;
        protected abstract string ClassName { get; }

        protected AbstractApiController(ILogger<TDerivedController> logger, 
                                        ControllerParams controllerParams)
        {
            //_logService = controllerParams.LogService;
            _logger = logger;
            _services = controllerParams.ServiceManager;
        }

        [NonAction]
        protected void LogRequestInfo()
        {
            //_logService.LogInfo(LogContentsTemplate.RequestInfo(Request.Method, Request.Path));
        }

        [NonAction]
        protected void LogResponseInfo()
        {
            string message = LogContentsTemplate.ResponseInfo(Response.StatusCode.ToString());
            if (Response.StatusCode is 
                400 or 401 or 402 or 403 or 404 or 405 or 406)
            {
                //_logService.LogWarning(message);
            }
            else
            {
                //_logService.LogInfo(message);
            }
            //_logService.LogInfo(LogContentsTemplate.SeparatorLine);
        }

        #region LOG FUNCTIONS

        [NonAction]
        protected void LogMethodInfo(string methodName)
        {
            //_logService.LogInfo(LogContentsTemplate.ControllerMethodInfo(this.ClassName, methodName));
        }

        [NonAction]
        protected void LogMethodReturnInfo(string result)
        {
            //_logService.LogInfo(LogContentsTemplate.ControllerMethodReturn(result));
        }

        private static string FormatContent(string content)
        {
            return LogContentsTemplate.ControllerFormat(content);
        }

        [NonAction]
        protected void LogInfo(string message)
        {
            //_logService.LogInfo(FormatContent(message));
        }

        [NonAction]
        protected void LogError(string message)
        {
            //_logService.LogError(FormatContent(message));
        }

        [NonAction]
        protected void LogDebug(string message)
        {
            //_logService.LogDebug(FormatContent(message));
        }

        [NonAction]
        protected void LogWarning(string message)
        {
            //_logService.LogWarning(FormatContent(message));
        }

        #endregion
    }
}
