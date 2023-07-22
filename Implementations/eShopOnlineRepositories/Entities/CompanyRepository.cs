namespace eShopOnlineRepositories.Entities
{
    internal sealed class CompanyRepository : AbstractRepository<Company>, ICompanyRepository
    {
        protected override string ChildClassName => nameof(CompanyRepository);

        internal CompanyRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Company> GetAll(bool isTrackChanges)
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            return base.FindAll(isTrackChanges);
        }

        public Company? GetById(bool isTrackChanges, Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(c => c.Id == id, isTrackChanges: false).Any();
        }
    }
}
