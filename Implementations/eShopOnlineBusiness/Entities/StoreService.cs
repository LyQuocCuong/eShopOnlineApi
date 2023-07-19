namespace eShopOnlineBusiness.Entities
{
    internal sealed class StoreService : AbstractService, IStoreService
    {
        public StoreService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<StoreDto> GetAll(bool isTrackChanges)
        {
            throw new NotImplementedException();
        }

        public StoreDto? GetById(bool isTrackChanges, Guid id)
        {
            throw new NotImplementedException();
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
