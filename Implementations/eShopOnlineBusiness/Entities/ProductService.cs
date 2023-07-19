namespace eShopOnlineBusiness.Entities
{
    internal sealed class ProductService : AbstractService, IProductService
    {
        public ProductService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<ProductDto> GetAll(bool isTrackChanges)
        {
            throw new NotImplementedException();
        }

        public ProductDto? GetById(bool isTrackChanges, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Create(ProductForCreationDto creationDto)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void HardDelete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
