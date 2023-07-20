namespace eShopOnlineRepositories.Entities
{
    internal sealed class StoreRepository : AbstractRepository<Store>, IStoreRepository
    {
        public StoreRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Store> GetAll(bool isTrackChanges)
        {
            return base.FindAll(isTrackChanges);
        }

        public Store? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(s => s.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            return base.FindByCondition(s => s.Id == id, isTrackChanges: false).Any();
        }

        public void Create(Store store)
        {
            base.CreateEntity(store);
        }       

        public void HardDelete(Store store)
        {
            base.HardDeleteEntity(store);
        }

        public void SoftDelete(Store store)
        {
            base.SoftDeleteEntity(store);
        }
    }
}
