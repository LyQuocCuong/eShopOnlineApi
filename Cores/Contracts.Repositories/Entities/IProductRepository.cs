namespace Contracts.Repositories.Entities
{
    public interface IProductRepository
    {
        Product? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Product> GetAll(bool isTrackChanges);

        void Create(Product product);

        void SoftDelete(Product product);

        void HardDelete(Product product);
    }
}
