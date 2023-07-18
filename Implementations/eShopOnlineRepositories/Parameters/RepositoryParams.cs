namespace eShopOnlineRepositories.Parameters
{
    public sealed class RepositoryParams
    {
        public readonly ShopOnlineContext Context;

        public RepositoryParams(ShopOnlineContext context)
        {
            Context = context;
        }

    }
}
