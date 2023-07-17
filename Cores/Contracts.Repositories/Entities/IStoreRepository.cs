namespace Contracts.Repositories.Entities
{
    public interface IStoreRepository
    {
        Store GetById(Guid id);

        IEnumerable<Store> GetAll();

        void Create(Store store);

        void Update(Store store);

        void Delete(Guid id);
    }
}
