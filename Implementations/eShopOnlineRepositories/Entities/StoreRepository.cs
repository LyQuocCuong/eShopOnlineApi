namespace eShopOnlineRepositories.Entities
{
    public sealed class StoreRepository : AbstractRepository<Store, StoreRepository>, IStoreRepository
    {
        internal StoreRepository(ILogger<StoreRepository> logger, 
                               RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
        {
        }

        public async Task<IEnumerable<Store>> GetAllAsync(bool isTrackChanges)
        {
            return await base.FindAll(isTrackChanges).ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(bool isTrackChanges, Guid id)
        {
            return await base.FindByCondition(s => s.Id == id, isTrackChanges).FirstOrDefaultAsync();
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await base.FindByCondition(s => s.Id == id, isTrackChanges: false).AnyAsync();
        }

        public async Task<Dictionary<DeleteStoreCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id)
        {
            _logger.LogInformation(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletionAsync)));
            return await CheckRequiredConditionsForDeletionAsync(id, DefaultDeleteEntityConditions.CheckListForDeletingAStore);
        }

        public async Task<Dictionary<DeleteStoreCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteStoreCondition> checkList)
        {
            var result = new Dictionary<DeleteStoreCondition, bool>();
            
            DeleteStoreCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingStore.IsExistingInDatabase);
            if (prerequisiteCondition != null)
            {
                result.Add(prerequisiteCondition, false);
                checkList.Remove(prerequisiteCondition);

                Store? store = await GetByIdAsync(isTrackChanges: false, id);
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
