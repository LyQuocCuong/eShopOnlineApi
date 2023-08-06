namespace eShopOnlineRepositories.Entities
{
    public sealed class StoreRepository : AbstractRepository<Store, StoreRepository>, IStoreRepository
    {
        internal StoreRepository(ILogger<StoreRepository> logger, 
                               RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
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

        public Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletingStore(Guid id)
        {
            _logger.LogInformation(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletingStore)));
            return CheckRequiredConditionsForDeletingStore(id, DefaultDeleteEntityConditions.CheckListForDeletingAStore);
        }

        public Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletingStore(Guid id, List<DeleteStoreCondition> checkList)
        {
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
                    _logger.LogInformation(RepositoryLogs.PassedCondition(prerequisiteCondition.ToString()));

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
                                _logger.LogWarning(RepositoryLogs.NotImplementedCondition(item.ToString()));
                                break;
                        }
                        if (result[item])
                        {
                            _logger.LogInformation(RepositoryLogs.PassedCondition(item.ToString()));
                        }
                        else
                        {
                            _logger.LogWarning(RepositoryLogs.FailedCondition(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    _logger.LogWarning(RepositoryLogs.ObjectNotExistInDB(nameof(Store), id));
                    _logger.LogWarning(RepositoryLogs.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                _logger.LogError(RepositoryLogs.MissingPrerequisiteCondition(ConditionsForDeletingStore.IsExistingInDatabase.ToString()));
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
