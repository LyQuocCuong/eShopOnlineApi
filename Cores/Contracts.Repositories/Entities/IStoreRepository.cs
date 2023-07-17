namespace Contracts.Repositories.Entities
{
    public interface IStoreRepository
    {
        Store? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Store> GetAll(bool isTrackChanges);

        void Create(Store store);

        void SoftDelete(Store store);

        void HardDelete(Store store);
    }
}
