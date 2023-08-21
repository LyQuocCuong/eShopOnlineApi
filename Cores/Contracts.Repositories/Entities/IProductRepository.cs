namespace Contracts.Repositories.Entities
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(bool isTrackChanges, Guid id);

        Task<IEnumerable<Product>> GetAllAsync(bool isTrackChanges);

        Task<bool> IsValidIdAsync(Guid id);

        Task<Dictionary<DeleteProductCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id);

        Task<Dictionary<DeleteProductCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteProductCondition> checkList);

        void Create(Product product);

        void DeleteSoftly(Product product);

        void DeleteHard(Product product);
    }
}
