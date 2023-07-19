namespace Contracts.Business.Entities
{
    public interface IStoreService
    {
        StoreDto? GetById(bool isTrackChanges, Guid id);

        IEnumerable<StoreDto> GetAll(bool isTrackChanges);

        void Create(StoreForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
