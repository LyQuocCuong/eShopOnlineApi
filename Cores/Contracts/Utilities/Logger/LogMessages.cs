namespace Contracts.Utilities.Logger
{
    public static class LogMessages
    {
        public static readonly string MessageForStartingMethodExecution = "START executing the method.";
        public static readonly string MessageForExecutingMethod = "EXECUTE the method.";
        public static readonly string MessageForFinishingMethodExecution = "FINISH executing the method.";

        public static readonly string MessageForNotImplementedCondition = "The condition has NOT been implemented yet.";
        public static readonly string MessageForExecutingWithDefaultCheckList = "Executing with the DEFAULT checklist.";
                
        public static string FormatMessageForObjectPassed(string message)
        {
            return $"(PASSED) - {message}";
        }

        public static string FormatMessageForObjectFailed(string message)
        {
            return $"(FAILED) - {message}";
        }                        

        public static string FormatMessageForObjectWithIdNotExistingInDatabase(string objectName, string id)
        {
            return $"The {objectName} object with Id:{id} does NOT exist in Database";
        }

        public static string FormatMessageForController(string className, string methodName, string message)
        {
            return $"[CONTROLLER] {className}/{methodName}() - {message}";
        }

        public static string FormatMessageForBusiness(string className, string methodName, string message)
        {
            return $"     |__[BUSINESS] {className}/{methodName}() - {message}";
        }

        public static string FormatMessageForMappingService(string sourceType, string destinationType)
        {
            return $"     |__[MAPPING] Mapping from [{sourceType}] to [{destinationType}]";
        }

        public static string FormatMessageForRepository(string className, string methodName, string message)
        {
            return $"            |__[REPOSITORY] {className}/{methodName}() - {message}";
        }

        public static string FormatMessageForEFCore(string message)
        {
            return $"                    |__[EFCORE] {message}";
        }

    }
}
