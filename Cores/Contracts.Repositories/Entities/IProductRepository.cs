namespace Contracts.Repositories.Entities
{
    public interface IProductRepository
    {
        Product? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Product> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<ConditionsForDeletingProduct, bool> CheckRequiredConditionsForDeletion(Guid id);

        Dictionary<ConditionsForDeletingProduct, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingProduct> checkList);

        void Create(Product product);

        void DeleteSoftly(Product product);

        void DeleteHard(Product product);
    }
}
