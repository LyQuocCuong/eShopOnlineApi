namespace eShopOnlineBusiness.LogMessages
{
    internal static class BusinessLogMessages
    {
        internal static string ObjectNotExistInDB(string objName, object condition)
        {
            return $"The {objName} object with {nameof(condition)}:{condition} does NOT exist in Database";
        }
    }
}
