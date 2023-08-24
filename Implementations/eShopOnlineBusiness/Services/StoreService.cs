namespace eShopOnlineBusiness.Services
{
    public sealed class StoreService : AbstractService<StoreService>, IStoreService
    {
        internal StoreService(ILogger<StoreService> logger, 
                              ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public async Task<IEnumerable<StoreDto>> GetAllAsync()
        {
            IEnumerable<Store> stores = await _repository.Store.GetAllAsync(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Store>, IEnumerable<StoreDto>>(stores);
        }

        public async Task<StoreDto?> GetByIdAsync(Guid id)
        {
            Store? store = await _repository.Store.GetByIdAsync(isTrackChanges: false, id);
            if (store == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Store), id));
                return null;
            }
            return _mapService.Execute<Store, StoreDto>(store);
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await _repository.Store.IsValidIdAsync(id);
        }

        public async Task<StoreDto> CreateAsync(StoreForCreationDto creationDto)
        {
            Store newStore = _mapService.Execute<StoreForCreationDto, Store>(creationDto);
            _repository.Store.Create(newStore);
            await _repository.SaveChangesAsync();

            return _mapService.Execute<Store, StoreDto>(newStore);
        }

        public async Task<bool> UpdateFullyAsync(Guid id, StoreForUpdateDto updateDto)
        {
            bool result = true;
            Store? store = await _repository.Store.GetByIdAsync(isTrackChanges: true, id);
            if (store != null)
            {
                _mapService.Execute<StoreForUpdateDto, Store>(updateDto, store);
                await _repository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Store), id));
                result = false;
            }
            return result;
        }

        private async Task<Dictionary<DeleteStoreCondition, bool>> CheckConditionsForDeletingAStoreAsync(Guid id, List<DeleteStoreCondition> checkList)
        {
            var result = new Dictionary<DeleteStoreCondition, bool>()
            {
                { DeleteStoreConditionDictionary.IsExistingInDatabase, false },
            };

            Store? Store = await _repository.Store.GetByIdAsync(isTrackChanges: false, id);
            if (Store == null)
            {
                return result;   // stop
            }

            result[DeleteStoreConditionDictionary.IsExistingInDatabase] = true;
            checkList.Remove(DeleteStoreConditionDictionary.IsExistingInDatabase);

            foreach (var item in checkList)
            {
                result.Add(item, false);
                switch (item.Condition)
                {
                    case DeleteStoreConditionsEnum.IsNotDeletedSoftly:
                        if (Store.IsDeleted == false)
                        {
                            result[item] = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public async Task<bool> DeleteSoftlyAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await CheckConditionsForDeletingAStoreAsync(id, DefaultRequiredConditions.DeleteAStore);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Store? store = await _repository.Store.GetByIdAsync(isTrackChanges: true, id);
                if (store != null)
                {
                    _repository.Store.DeleteSoftly(store);
                    await _repository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Store), id));
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> DeleteHardAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await CheckConditionsForDeletingAStoreAsync(id, DefaultRequiredConditions.DeleteAStore);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Store? store = await _repository.Store.GetByIdAsync(isTrackChanges: true, id);
                if (store != null)
                {
                    _repository.Store.DeleteHard(store);
                    await _repository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Store), id));
                    result = false;
                }
            }
            return result;
        }

    }
}
