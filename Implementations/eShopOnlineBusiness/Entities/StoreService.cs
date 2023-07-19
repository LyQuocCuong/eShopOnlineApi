﻿namespace eShopOnlineBusiness.Entities
{
    internal sealed class StoreService : AbstractService, IStoreService
    {
        public StoreService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<StoreDto> GetAll()
        {
            IEnumerable<Store> stores = _repository.Store.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Store>, IEnumerable<StoreDto>>(stores);
        }

        public StoreDto? GetById(Guid id)
        {
            Store? store = _repository.Store.GetById(isTrackChanges: false, id);
            if (store == null)
            {
                return null;
            }
            return _mapperService.Execute<Store, StoreDto>(store);
        }

        public void Create(StoreForCreationDto creationDto)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void HardDelete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
