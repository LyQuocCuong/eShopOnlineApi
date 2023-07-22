namespace Contracts.Utilities.Logger
{
    public static class LogMessages
    {
        public static readonly string MessageForStartingMethodExecution = "START executing the method.";
        public static readonly string MessageForExecutingMethod = "EXECUTE the method.";
        public static readonly string MessageForFinishingMethodExecution = "FINISH executing the method.";

        public static readonly string MessageForExecutingWithDefaultCheckList = "Executing with the DEFAULT checklist.";
        public static readonly string MessageForSettingAllConditionsInCheckListToFalse = "All of the Conditions in CheckList will be set to FALSE.";

        public static readonly string MessageForHardDeleteSuccess = "";

        public static string FormatMessageForValidatingItemPassed(string item)
        {
            return $"{item} - PASSED";
        }

        public static string FormatMessageForValidatingItemFailed(string item)
        {
            return $"{item} - FAILED";
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
            return $"___|___[BUSINESS] {className}/{methodName}() - {message}";
        }

        public static string FormatMessageForMappingService(string sourceType, string destinationType)
        {
            return $"___|___[MAPPING] Mapping from [{sourceType}] to [{destinationType}]";
        }

        public static string FormatMessageForRepository(string className, string methodName, string message)
        {
            return $"_______|___[REPOSITORY] {className}/{methodName}() - {message}";
        }

        public static string FormatMessageForEFCore(string message)
        {
            return $"___________|___[EFCORE] {message}";
        }

    }
}
