namespace eShopOnlineRepositories.Parameters
{
    public sealed class RepositoryParams
    {
        public readonly ShopOnlineContext Context;
        public readonly ILogService LogService;

        public RepositoryParams(ShopOnlineContext context, 
                                ILogService logService)
        {
            Context = context;
            LogService = logService;
        }

    }
}
