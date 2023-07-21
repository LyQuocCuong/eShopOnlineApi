namespace Contracts.Repositories.Entities
{
    public interface IStoreRepository
    {
        Store? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Store> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<ConditionsForDeletingStore, bool> CheckRequiredConditionsForDeletion(Guid id);

        Dictionary<ConditionsForDeletingStore, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingStore> checkList);

        void Create(Store store);

        void DeleteSoftly(Store store);

        void DeleteHard(Store store);
    }
}
