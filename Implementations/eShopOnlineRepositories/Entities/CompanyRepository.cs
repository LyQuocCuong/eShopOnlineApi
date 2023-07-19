namespace eShopOnlineRepositories.Entities
{
    internal sealed class CompanyRepository : AbstractRepository<Company>, ICompanyRepository
    {
        internal CompanyRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Company> GetAll(bool isTrackChanges)
        {
            return base.FindAll(isTrackChanges);
        }

        public Company? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }
    }
}
