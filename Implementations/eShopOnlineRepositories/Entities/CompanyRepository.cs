namespace eShopOnlineRepositories.Entities
{
    public sealed class CompanyRepository : AbstractRepository<Company, CompanyRepository>, ICompanyRepository
    {
        internal CompanyRepository(ILogger<CompanyRepository> logger, 
                                   RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
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

        public bool IsValidId(Guid id)
        {
            bool result = base.FindByCondition(c => c.Id == id, isTrackChanges: false).Any();
            return result;
        }
    }
}
