using Shared.Templates;

namespace eShopOnlineBusiness.LogMessages
{
    internal static class BusinessLogs
    {
        private static string FormatMessage(string message)
        {
            return LogContentsTemplate.BusinesFormat(message);
        }

        internal static string ObjectNotExistInDB(string objName, object value)
        {
            return FormatMessage($"The {objName} object with VALUE:{value} does NOT exist in Database");
        }
    }
}
