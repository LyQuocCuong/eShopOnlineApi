namespace Contracts.Business.Entities
{
    public interface IStoreService
    {
        StoreDto? GetById(Guid id);

        IEnumerable<StoreDto> GetAll();

        void Create(StoreForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
