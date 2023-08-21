namespace eShopOnlineRepositories.Entities
{
    public sealed class CompanyRepository : AbstractRepository<Company, CompanyRepository>, ICompanyRepository
    {
        internal CompanyRepository(ILogger<CompanyRepository> logger, 
                                   RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
        {
        }

        public async Task<IEnumerable<Company>> GetAllAsync(bool isTrackChanges)
        {
            return await base.FindAll(isTrackChanges).ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(bool isTrackChanges, Guid id)
        {
            return await base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefaultAsync();
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await base.FindByCondition(c => c.Id == id, isTrackChanges: false).AnyAsync(); ;
        }
    }
}
