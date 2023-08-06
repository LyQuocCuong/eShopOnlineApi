using Shared.Templates;

namespace eShopOnlineRepositories.LogMessages
{
    internal static class RepositoryLogs
    {
        private static string FormatMessage(string message)
        {
            return LogContentsTemplate.RepositoryFormat(message);
        }

        internal static string NotImplementedCondition(string condition)
        {
            return FormatMessage($"Condition: NOT Implemented - {condition}.");
        }

        internal static string PassedCondition(string condition)
        {
            return FormatMessage($"Condition: PASSED - {condition}.");
        }

        internal static string FailedCondition(string condition)
        {
            return FormatMessage($"Condition: FAILED - {condition}.");
        }

        internal static string MissingPrerequisiteCondition(string condition)
        {
            return FormatMessage($"Prerequisite Condition: MISSING - {condition}.");
        }

        internal static string ObjectNotExistInDB(string objName, object condition)
        {
            return FormatMessage($"The {objName} object with {nameof(condition)}:{condition} does NOT exist in Database");
        }

    }
}
