namespace eShopOnlineBusiness.Entities
{
    public sealed class StoreService : AbstractService<StoreService>, IStoreService
    {
        internal StoreService(ILogger<StoreService> logger, 
                              ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public IEnumerable<StoreDto> GetAll()
        {
            IEnumerable<Store> stores = _repository.Store.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Store>, IEnumerable<StoreDto>>(stores);
        }

        public StoreDto? GetById(Guid id)
        {
            Store? store = _repository.Store.GetById(isTrackChanges: false, id);
            if (store == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Store), id));
                return null;
            }
            return _mapService.Execute<Store, StoreDto>(store);
        }

        public bool IsValidId(Guid id)
        {
            return _repository.Store.IsValidId(id);
        }

        public StoreDto Create(StoreForCreationDto creationDto)
        {
            Store newStore = _mapService.Execute<StoreForCreationDto, Store>(creationDto);
            _repository.Store.Create(newStore);
            _repository.SaveChanges();

            return _mapService.Execute<Store, StoreDto>(newStore);
        }

        public bool UpdateFully(Guid id, StoreForUpdateDto updateDto)
        {
            bool result = true;
            Store? store = _repository.Store.GetById(isTrackChanges: true, id);
            if (store != null)
            {
                _mapService.Execute<StoreForUpdateDto, Store>(updateDto, store);
                _repository.SaveChanges();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Store), id));
                result = false;
            }
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Store.CheckRequiredConditionsForDeletingStore(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Store? store = _repository.Store.GetById(isTrackChanges: true, id);
                if (store != null)
                {
                    _repository.Store.DeleteSoftly(store);
                    _repository.SaveChanges();
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

        public bool DeleteHard(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Store.CheckRequiredConditionsForDeletingStore(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Store? store = _repository.Store.GetById(isTrackChanges: true, id);
                if (store != null)
                {
                    _repository.Store.DeleteHard(store);
                    _repository.SaveChanges();
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
