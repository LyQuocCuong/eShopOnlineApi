namespace Contracts.Repositories.Entities
{
    public interface IProductRepository
    {
        Product? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Product> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<DeleteProductCondition, bool> CheckRequiredConditionsForDeletingProduct(Guid id);

        Dictionary<DeleteProductCondition, bool> CheckRequiredConditionsForDeletingProduct(Guid id, List<DeleteProductCondition> checkList);

        void Create(Product product);

        void DeleteSoftly(Product product);

        void DeleteHard(Product product);
    }
}
