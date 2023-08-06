namespace eShopOnlineBusiness.Entities
{
    public sealed class ProductService : AbstractService<ProductService>, IProductService
    {
        internal ProductService(ILogger<ProductService> logger, 
                                ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public IEnumerable<ProductDto> GetAll()
        {
            IEnumerable<Product> products = _repository.Product.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public ProductDto? GetById(Guid id)
        {
            Product? product = _repository.Product.GetById(isTrackChanges: false, id);
            if(product == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Product), id));
                return null;
            }
            return _mapService.Execute<Product, ProductDto>(product);
        }

        public bool IsValidId(Guid id)
        {
            return _repository.Product.IsValidId(id);
        }

        public ProductDto Create(ProductForCreationDto creationDto)
        {
            Product newProduct = _mapService.Execute<ProductForCreationDto, Product>(creationDto);
            _repository.Product.Create(newProduct);
            _repository.SaveChanges();

            return _mapService.Execute<Product, ProductDto>(newProduct);
        }

        public bool UpdateFully(Guid id, ProductForUpdateDto updateDto)
        {
            bool result = true;
            Product? product = _repository.Product.GetById(isTrackChanges: true, id);
            if (product != null)
            {
                _mapService.Execute<ProductForUpdateDto, Product>(updateDto, product);
                _repository.SaveChanges();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Product), id));
                result = false;
            }
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Product.CheckRequiredConditionsForDeletingProduct(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Product? product = _repository.Product.GetById(isTrackChanges: true, id);
                if (product != null)
                {
                    _repository.Product.DeleteSoftly(product);
                    _repository.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Product), id));
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Product.CheckRequiredConditionsForDeletingProduct(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Product? product = _repository.Product.GetById(isTrackChanges: true, id);
                if (product != null)
                {
                    _repository.Product.DeleteHard(product);
                    _repository.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Product), id));
                    result = false;
                }
            }
            return result;
        }

    }
}
