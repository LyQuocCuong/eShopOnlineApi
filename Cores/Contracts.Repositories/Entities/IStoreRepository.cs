namespace Contracts.Repositories.Entities
{
    public interface IStoreRepository
    {
        Store? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Store> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletion(Guid id);

        Dictionary<DeleteStoreCondition, bool> CheckRequiredConditionsForDeletion(Guid id, List<DeleteStoreCondition> checkList);

        void Create(Store store);

        void DeleteSoftly(Store store);

        void DeleteHard(Store store);
    }
}
