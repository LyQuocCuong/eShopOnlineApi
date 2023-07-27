using Shared.Templates;

namespace eShopOnlineUtilities.LogMessages
{
    internal static class MapLogMessages
    {
        private static bool IsCollectionType(Type genericType)
        {
            return (typeof(System.Collections.IEnumerable).IsAssignableFrom(genericType));
        }

        private static string GetFullCollectionTypeName(Type genericType)
        {
            /// Example: genericType = typeof(IEnumerable<Employee>)

            // genericType.Name -> "IEnumerable`1" => IEnumerable
            string collectionType = genericType.Name.Split('`')[0];

            // GetGenericArguments() -> [0]: {Name = "Employee", FullName = "Domains.Entities.Employee"}
            string objType = genericType.GetGenericArguments()[0].Name;

            return $"{collectionType}<{objType}>";
        }

        public static string MappingInfo<TSource, TDestination>()
        {
            string sourceType = typeof(TSource).Name;
            string destinationType = typeof(TDestination).Name;

            if (IsCollectionType(typeof(TSource)))
            {
                sourceType = GetFullCollectionTypeName(typeof(TSource));
            }

            if (IsCollectionType(typeof(TDestination)))
            {
                destinationType = GetFullCollectionTypeName(typeof(TDestination));
            }

            string content = $"Mapping from {sourceType} to {destinationType}";
            return LogContentsTemplate.MapServiceFormat(content);
        }
    }
}
