namespace eShopOnlineRepositories.Entities
{
    internal sealed class StoreRepository : AbstractRepository<Store>, IStoreRepository
    {
        protected override string ClassName => nameof(StoreRepository);

        public StoreRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Store> GetAll(bool isTrackChanges)
        {
            LogMethodInfo(nameof(GetAll));
            return base.FindAll(isTrackChanges);
        }

        public Store? GetById(bool isTrackChanges, Guid id)
        {
            LogMethodInfo(nameof(GetById));
            return base.FindByCondition(s => s.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));
            return base.FindByCondition(s => s.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletingStore(Guid id)
        {
            LogMethodInfo(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletingStore)));
            return CheckRequiredConditionsForDeletingStore(id, DefaultDeleteEntityConditions.CheckListForDeletingAStore);
        }

        public Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletingStore(Guid id, List<DeleteStoreCondition> checkList)
        {
            LogMethodInfo(nameof(CheckRequiredConditionsForDeletingStore));

            var result = new Dictionary<DeleteStoreCondition, bool>();
            
            DeleteStoreCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingStore.IsExistingInDatabase);
            if (prerequisiteCondition != null)
            {
                result.Add(prerequisiteCondition, false);
                checkList.Remove(prerequisiteCondition);

                Store? store = base.FindByCondition(s => s.Id == id, isTrackChanges: false).FirstOrDefault();
                if (store != null)
                {
                    result[prerequisiteCondition] = true;
                    LogInfo(RepositoryLogMessages.PassedCondition(prerequisiteCondition.ToString()));

                    // checking Other Conditions
                    foreach (var item in checkList)
                    {
                        result.Add(item, false);
                        switch (item.Condition)
                        {
                            case ConditionsForDeletingStore.IsNotDeletedSoftly:
                                if (store.IsDeleted == false)
                                {
                                    result[item] = true;
                                }
                                break;
                            default:
                                LogWarning(RepositoryLogMessages.NotImplementedCondition(item.ToString()));
                                break;
                        }
                        if (result[item])
                        {
                            LogInfo(RepositoryLogMessages.PassedCondition(item.ToString()));
                        }
                        else
                        {
                            LogInfo(RepositoryLogMessages.FailedCondition(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    LogInfo(RepositoryLogMessages.ObjectNotExistInDB(nameof(Store), id));
                    LogInfo(RepositoryLogMessages.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                LogInfo(RepositoryLogMessages.MissingPrerequisiteCondition(ConditionsForDeletingStore.IsExistingInDatabase.ToString()));
            }
            return result;
        }

        public void Create(Store store)
        {
            LogMethodInfo(nameof(Create));
            base.CreateEntity(store);
        }       

        public void DeleteSoftly(Store store)
        {
            LogMethodInfo(nameof(DeleteSoftly));
            base.DeleteEntitySoftly(store);
        }

        public void DeleteHard(Store store)
        {
            LogMethodInfo(nameof(DeleteHard));
            base.DeleteEntityHard(store);
        }

    }
}
