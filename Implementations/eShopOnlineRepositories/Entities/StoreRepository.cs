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

        public Dictionary<ConditionsForDeletingStore, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfStoreDeletion);
        }

        public Dictionary<ConditionsForDeletingStore, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingStore> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForStartingMethodExecution);
            Store? store = base.FindByCondition(s => s.Id == id, isTrackChanges: false).FirstOrDefault();
            if (store == null)
            {
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Store), id.ToString()));
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForSettingAllConditionsInCheckListToFalse);
            }
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
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - PASSED. The Store has not been deleted softly yet.");
                                result[condition] = true;
                            }
                            else
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the Store has been deleted softly (IsDeleted = TRUE).");
                            }
                            break;
                        default:
                            LogWarning(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. The checking condition has NOT been implemented yet.");
                            break;
                    }
                }
            }
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForFinishingMethodExecution);
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
