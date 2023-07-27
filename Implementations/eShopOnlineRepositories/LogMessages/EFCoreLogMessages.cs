using eShopOnlineRepositories.Helpers;
using Shared.Templates;

namespace eShopOnlineRepositories.LogMessages
{
    internal static class EFCoreLogMessages
    {
        private static string FormatContent(string content)
        {
            return LogContentsTemplate.EFCoreFormat(content);
        }

        internal static string Message(string message)
        {
            return FormatContent($"{message}");
        }

        internal static string QueryTracking(Expression expressionTree)
        {
            string expression = LogExpressionTreeHelper.PrintExpression(expressionTree);
            return FormatContent($"(Tracking) Expression: {expression}");
        }

        internal static string QueryNoTracking(Expression expressionTree)
        {
            string expression = LogExpressionTreeHelper.PrintExpression(expressionTree);
            return FormatContent($"(No-Tracking) Expression: {expression}");
        }

        internal static string QueryTrackingFindAll = FormatContent($"(Tracking) FindAll()");

        internal static string QueryNoTrackingFindAll = FormatContent($"(No-Tracking) FindAll()");

        internal static string Create = FormatContent($"(Create) Change EntityState.Added");

        internal static string SoftDelete = FormatContent($"(Soft-Delete) Set (IsDeleted) to TRUE");

        internal static string HardDelete = FormatContent($"(Hard-Delete) Change EntityState.Deleted");

        internal static string BeginSaving = FormatContent($"(Saving) - Begin");

        internal static string EndSaving = FormatContent($"(Saving) - End");
    }
}
