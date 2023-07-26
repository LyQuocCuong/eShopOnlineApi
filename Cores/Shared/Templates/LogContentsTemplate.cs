namespace Shared.Templates
{
    public static class LogContentsTemplate
    {
        public static string RequestInfo(string httpMethod, string path)                                            => $"[REQUEST] {httpMethod} - PATH: {path}";
        public static string ControllerMethodInfo(string className, string methodName)                              => $"[CONTROLLER] {className}/{methodName}() - EXECUTING";
        public static string ControllerFormat(string controllerContent)                                             => $"    |--{controllerContent}";
        public static string BusinessMethodInfo(string className, string methodName)                                => $"    |--[BUSINESS] {className}/{methodName}() - EXECUTING";
        public static string BusinesFormat(string businessContent)                                                  => $"           |--{businessContent}";
        public static string RepositoryMethodInfo(string className, string methodName)                              => $"           |--[REPOSITORY] {className}/{methodName}() - EXECUTING";
        public static string RepositoryFormat(string repoContent)                                                   => $"                  |--{repoContent}";
        public static string EFCoreFormat(string efcoreContent)                                                     => $"                  |--[EFCORE] {efcoreContent}";
        public static string RepositoryMethodReturn(string repoResult)                                              => $"           |--[REPOSITORY] RETURN: {repoResult}";
        public static string MapServiceFormat(string mapContent)                                                    => $"    |--[MAPSERVICE] {mapContent}";
        public static string BusinessMethodReturn(string businessResult)                                            => $"    |--[BUSINESS] RETURN: {businessResult}";
        public static string ControllerMethodReturn(string apiResult)                                               => $"[CONTROLLER] RETURN: {apiResult}";
        public static string ResponseInfo(string statusCode)                                                        => $"[RESPONSE] STATUS CODE - {statusCode}";
        public static string SeparatorLine                                                                          = "===========================================================================================";
    }
}
