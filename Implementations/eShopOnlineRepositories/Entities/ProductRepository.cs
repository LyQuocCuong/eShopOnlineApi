namespace eShopOnlineRepositories.Entities
{
    internal sealed class ProductRepository : AbstractRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopOnlineContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAll(bool isTrackChanges)
        {
            return base.FindAll(isTrackChanges);
        }

        public Product? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(p => p.Id == id, isTrackChanges).FirstOrDefault();
        }

        public void Create(Product product)
        {
            base.CreateEntity(product);
        }        

        public void HardDelete(Product product)
        {
            base.HardDeleteEntity(product);
        }

        public void SoftDelete(Product product)
        {
            base.SoftDeleteEntity(product);
        }
    }
}
