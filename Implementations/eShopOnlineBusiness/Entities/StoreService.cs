namespace eShopOnlineBusiness.Entities
{
    internal sealed class StoreService : AbstractService, IStoreService
    {
        protected override string ClassName => nameof(StoreService);

        public StoreService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<StoreDto> GetAll()
        {
            LogMethodInfo(nameof(GetAll));
            IEnumerable<Store> stores = _repository.Store.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Store>, IEnumerable<StoreDto>>(stores);
        }

        public StoreDto? GetById(Guid id)
        {
            LogMethodInfo(nameof(GetById));
            Store? store = _repository.Store.GetById(isTrackChanges: false, id);
            if (store == null)
            {
                LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Store), id));
                return null;
            }
            return _mapService.Execute<Store, StoreDto>(store);
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));
            return _repository.Store.IsValidId(id);
        }

        public StoreDto Create(StoreForCreationDto creationDto)
        {
            LogMethodInfo(nameof(Create));

            Store newStore = _mapService.Execute<StoreForCreationDto, Store>(creationDto);
            _repository.Store.Create(newStore);
            _repository.SaveChanges();

            return _mapService.Execute<Store, StoreDto>(newStore);
        }

        public bool UpdateFully(Guid id, StoreForUpdateDto updateDto)
        {
            LogMethodInfo(nameof(UpdateFully));

            bool result = true;
            Store? store = _repository.Store.GetById(isTrackChanges: true, id);
            if (store != null)
            {
                _mapService.Execute<StoreForUpdateDto, Store>(updateDto, store);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Store), id));
                result = false;
            }
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            LogMethodInfo(nameof(DeleteSoftly));

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
                    LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Store), id));
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            LogMethodInfo(nameof(DeleteHard));

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
                    LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Store), id));
                    result = false;
                }
            }
            return result;
        }

    }
}
