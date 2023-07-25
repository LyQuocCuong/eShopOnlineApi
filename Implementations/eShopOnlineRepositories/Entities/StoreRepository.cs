namespace eShopOnlineRepositories.Entities
{
    internal sealed class StoreRepository : AbstractRepository<Store>, IStoreRepository
    {
        protected override string ChildClassName => nameof(StoreRepository);

        public StoreRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Store> GetAll(bool isTrackChanges)
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            return base.FindAll(isTrackChanges);
        }

        public Store? GetById(bool isTrackChanges, Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(s => s.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(s => s.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, DefaultDeleteEntityConditions.CheckListForDeletingAStore);
        }

        public Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletion(Guid id, List<DeleteStoreCondition> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingMethod);

            var result = new Dictionary<DeleteStoreCondition, bool>();
            
            Store? store = base.FindByCondition(s => s.Id == id, isTrackChanges: false).FirstOrDefault();
            DeleteStoreCondition? isExistingInDatabaseCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingStore.IsExistingInDatabase);

            if (isExistingInDatabaseCondition != null)
            {
                result.Add(isExistingInDatabaseCondition, false);
                checkList.Remove(isExistingInDatabaseCondition);
                if (store != null)
                {
                    result[isExistingInDatabaseCondition] = true;
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(isExistingInDatabaseCondition.ToString()));

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
                                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(LogMessages.MessageForNotImplementedCondition));
                                break;
                        }
                        if (result[item])
                        {
                            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(item.ToString()));
                        }
                        else
                        {
                            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Store), id.ToString()));
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(isExistingInDatabaseCondition.ToString()));
                }
            }
            else
            {
                LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed("Missing IsExistingInDatabase Condition."));
            }
            return result;
        }

        public void Create(Store store)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);
            base.CreateEntity(store);
        }       

        public void DeleteSoftly(Store store)
        {
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForExecutingMethod);
            base.DeleteEntitySoftly(store);
        }

        public void DeleteHard(Store store)
        {
            LogInfo(nameof(DeleteHard), LogMessages.MessageForExecutingMethod);
            base.DeleteEntityHard(store);
        }

    }
}
