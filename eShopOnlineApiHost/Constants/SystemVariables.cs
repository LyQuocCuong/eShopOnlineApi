namespace eShopOnlineApiHost.Constants
{
    public static class SystemVariables
    {
        public static readonly string EShopConnectionString = "eShopOnlineConnection";
        public static readonly string SolutionEShopOnlineApiPath = Directory.GetCurrentDirectory().Replace("eShopOnlineApiHost", "");
        public static readonly string PathNLogConfigFile = String.Concat(SolutionEShopOnlineApiPath, "Implementations\\eShopOnlineUtilities\\NLog\\NLog.config");

        public static readonly string NLogConfigFilePath = String.Concat(SolutionEShopOnlineApiPath, $".\\Implementations\\eShopOnlineUtilities\\NLog\\nlog.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.config");

    }
}
