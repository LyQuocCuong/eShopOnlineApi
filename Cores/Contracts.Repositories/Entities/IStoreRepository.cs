namespace Contracts.Repositories.Entities
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(bool isTrackChanges, Guid id);

        Task<IEnumerable<Store>> GetAllAsync(bool isTrackChanges);

        Task<bool> IsValidIdAsync(Guid id);

        //Task<Dictionary<DeleteStoreCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id);

        //Task<Dictionary<DeleteStoreCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteStoreCondition> checkList);

        void Create(Store store);

        void DeleteSoftly(Store store);

        void DeleteHard(Store store);
    }
}
