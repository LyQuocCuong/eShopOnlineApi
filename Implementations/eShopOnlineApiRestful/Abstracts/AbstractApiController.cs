using Contracts.Business.Managers;

namespace eShopOnlineApiRestful.Abstracts
{
    [ApiController]
    [Route("api")]
    public abstract class AbstractApiController : ControllerBase
    {
        private readonly ILogService _logService;
        protected readonly IServiceManager _services;
        protected abstract string ChildClassName { get; }

        protected AbstractApiController(ControllerParams controllerParams)
        {
            _logService = controllerParams.LogService;
            _services = controllerParams.ServiceManager;
        }

        [NonAction]
        protected void LogInfoRequest()
        {
            _logService.LogInfo("=============================================================");
            _logService.LogInfo($"[REQUEST][METHOD - {Request.Method}] PATH - {Request.Path}");
        }

        [NonAction]
        protected void LogInfoResponse()
        {
            if (Response.StatusCode is 
                400 or 401 or 402 or 403 or 404 or 405 or 406)
            {
                _logService.LogWarning($"[RESPONSE][STATUS CODE - {Response.StatusCode}]");
            }
            else
            {
                _logService.LogInfo($"[RESPONSE][STATUS CODE - {Response.StatusCode}]");
            }
        }

        [NonAction]
        private string GenerateMessages(string methodName, string message)
        {
            return LogMessages.FormatMessageForController(ChildClassName, methodName, message);
        }

        [NonAction]
        protected void LogDebug(string methodName, string message)
        {
            _logService.LogDebug(GenerateMessages(methodName, message));
        }

        [NonAction]
        protected void LogError(string methodName, string message)
        {
            _logService.LogError(GenerateMessages(methodName, message));
        }

        [NonAction]
        protected void LogInfo(string methodName, string message)
        {
            _logService.LogInfo(GenerateMessages(methodName, message));
        }

        [NonAction]
        protected void LogWarning(string methodName, string message)
        {
            _logService.LogWarning(GenerateMessages(methodName, message));
        }

    }
}
