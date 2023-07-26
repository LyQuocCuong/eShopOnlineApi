namespace eShopOnlineRepositories.LogMessages
{
    internal static class RepositoryLogMessages
    {
        internal static string NotImplementedCondition(string condition)
        {
            return $"Condition: NOT Implemented - {condition}.";
        }

        internal static string PassedCondition(string condition)
        {
            return $"Condition: PASSED - {condition}.";
        }

        internal static string FailedCondition(string condition)
        {
            return $"Condition: FAILED - {condition}.";
        }

        internal static string MissingPrerequisiteCondition(string condition)
        {
            return $"Prerequisite Condition: MISSING - {condition}.";
        }

        internal static string ObjectNotExistInDB(string objName, object condition)
        {
            return $"The {objName} object with {nameof(condition)}:{condition} does NOT exist in Database";
        }

    }
}
