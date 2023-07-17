namespace eShopOnlineRepositories.Entities
{
    internal sealed class CompanyRepository : AbstractRepository<Company>, ICompanyRepository
    {
        internal CompanyRepository(ShopOnlineContext context) : base(context)
        {
        }

        public Company? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }
    }
}
