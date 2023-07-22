namespace eShopOnlineApiHost.Constants
{
    public static class SystemVariables
    {
        public static readonly string EShopConnectionString = "eShopOnlineConnection";
        public static readonly string PathEShopSolution = Directory.GetCurrentDirectory().Replace("eShopOnlineApiHost", "");
        public static readonly string PathNLogConfigFile = String.Concat(PathEShopSolution, "Implementations\\eShopOnlineUtilities\\NLog\\NLog.config");
    }
}
