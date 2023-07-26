using Shared.Templates;

namespace eShopOnlineUtilities.LogContents
{
    internal static class MapLogContent
    {
        public static string MappingInfo<TSource, TDestination>()
        {
            string sourceType = typeof(TSource).Name;
            string destinationType = typeof(TDestination).Name;

            string content = $"Mapping from [{sourceType}] to [{destinationType}]";
            return LogContentsTemplate.MapServiceFormat(content);
        }
    }
}
