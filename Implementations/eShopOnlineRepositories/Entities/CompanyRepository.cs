namespace eShopOnlineRepositories.Entities
{
    internal sealed class CompanyRepository : AbstractRepository<Company>, ICompanyRepository
    {
        internal CompanyRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public Company? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }
    }
}
