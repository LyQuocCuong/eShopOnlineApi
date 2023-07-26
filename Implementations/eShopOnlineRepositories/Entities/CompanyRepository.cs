namespace eShopOnlineRepositories.Entities
{
    internal sealed class CompanyRepository : AbstractRepository<Company>, ICompanyRepository
    {
        protected override string ClassName => nameof(CompanyRepository);

        internal CompanyRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Company> GetAll(bool isTrackChanges)
        {
            LogMethodInfo(nameof(GetAll));
            return base.FindAll(isTrackChanges);
        }

        public Company? GetById(bool isTrackChanges, Guid id)
        {
            LogMethodInfo(nameof(GetById));
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));

            bool result = base.FindByCondition(c => c.Id == id, isTrackChanges: false).Any();

            LogMethodReturnInfo(result.ToString());
            return result;
        }
    }
}
