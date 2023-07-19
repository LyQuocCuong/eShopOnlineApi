namespace eShopOnlineBusiness.Entities
{
    internal sealed class ProductService : AbstractService, IProductService
    {
        public ProductService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<ProductDto> GetAll()
        {
            IEnumerable<Product> products = _repository.Product.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public ProductDto? GetById(Guid id)
        {
            Product? product = _repository.Product.GetById(isTrackChanges: false, id);
            if(product == null)
            {
                return null;
            }
            return _mapperService.Execute<Product, ProductDto>(product);
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
