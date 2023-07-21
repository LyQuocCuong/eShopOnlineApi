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

        public Dictionary<ConditionsForDeletingStore, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfStoreDeletion);
        }

        public Dictionary<ConditionsForDeletingStore, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingStore> checkList)
        {
            Store? store = base.FindByCondition(s => s.Id == id, isTrackChanges: false).FirstOrDefault();
            var result = new Dictionary<ConditionsForDeletingStore, bool>();

            foreach(var condition in checkList)
            {
                result.Add(condition, false);
                if (store != null)
                {
                    switch (condition)
                    {
                        case ConditionsForDeletingStore.IsNotDeletedSoftly:
                            if (store.IsDeleted == false)
                            {
                                result[condition] = true;
                            }
                            break;
                    }
                }
            }
            return result;
        }

        public void Create(Store store)
        {
            base.CreateEntity(store);
        }       

        public void DeleteSoftly(Store store)
        {
            base.DeleteEntitySoftly(store);
        }

        public void DeleteHard(Store store)
        {
            base.DeleteEntityHard(store);
        }

    }
}
