namespace Contracts.Business.Entities
{
    public interface IStoreService
    {
        StoreDto? GetById(Guid id);

        IEnumerable<StoreDto> GetAll();

        bool IsValidId(Guid id);

        StoreDto Create(StoreForCreationDto creationDto);

        bool UpdateFully(Guid id, StoreForUpdateDto updateDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
