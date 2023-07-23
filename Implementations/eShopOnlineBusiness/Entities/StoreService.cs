namespace eShopOnlineBusiness.Entities
{
    internal sealed class StoreService : AbstractService, IStoreService
    {
        protected override string ChildClassName => nameof(StoreService);

        public StoreService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<StoreDto> GetAll()
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            IEnumerable<Store> stores = _repository.Store.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Store>, IEnumerable<StoreDto>>(stores);
        }

        public StoreDto? GetById(Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            Store? store = _repository.Store.GetById(isTrackChanges: false, id);
            if (store == null)
            {
                LogInfo(nameof(GetById), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Store), id.ToString()));
                return null;
            }
            return _mapperService.Execute<Store, StoreDto>(store);
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return _repository.Store.IsValidId(id);
        }

        public StoreDto Create(StoreForCreationDto creationDto)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);

            Store newStore = _mapperService.Execute<StoreForCreationDto, Store>(creationDto);
            _repository.Store.Create(newStore);
            _repository.SaveChanges();

            return _mapperService.Execute<Store, StoreDto>(newStore);
        }

        public bool UpdateFully(Guid id, StoreForUpdateDto updateDto)
        {
            bool result = true;
            LogInfo(nameof(UpdateFully), LogMessages.MessageForStartingMethodExecution);
            Store? store = _repository.Store.GetById(isTrackChanges: true, id);
            if (store != null)
            {
                _mapperService.Execute<StoreForUpdateDto, Store>(updateDto, store);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(nameof(UpdateFully), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Store), id.ToString()));
                result = false;
            }            
            LogInfo(nameof(UpdateFully), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForStartingMethodExecution);
            var resultCheckList = _repository.Store.CheckRequiredConditionsForDeletion(id);
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
                    LogInfo(nameof(DeleteSoftly), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Store), id.ToString()));
                    result = false;
                }
            }
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            bool result = true;
            LogInfo(nameof(DeleteHard), LogMessages.MessageForStartingMethodExecution);
            var resultCheckList = _repository.Store.CheckRequiredConditionsForDeletion(id);
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
                    LogInfo(nameof(DeleteHard), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Store), id.ToString()));
                    result = false;
                }
            }
            LogInfo(nameof(DeleteHard), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

    }
}
