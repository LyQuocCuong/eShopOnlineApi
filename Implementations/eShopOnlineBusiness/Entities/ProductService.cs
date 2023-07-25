namespace eShopOnlineBusiness.Entities
{
    internal sealed class ProductService : AbstractService, IProductService
    {
        protected override string ChildClassName => nameof(ProductService);

        public ProductService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<ProductDto> GetAll()
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            IEnumerable<Product> products = _repository.Product.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public ProductDto? GetById(Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            Product? product = _repository.Product.GetById(isTrackChanges: false, id);
            if(product == null)
            {
                LogInfo(nameof(GetById), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Product), id.ToString()));
                return null;
            }
            return _mapService.Execute<Product, ProductDto>(product);
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return _repository.Product.IsValidId(id);
        }

        public ProductDto Create(ProductForCreationDto creationDto)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);

            Product newProduct = _mapService.Execute<ProductForCreationDto, Product>(creationDto);
            _repository.Product.Create(newProduct);
            _repository.SaveChanges();

            return _mapService.Execute<Product, ProductDto>(newProduct);
        }

        public bool UpdateFully(Guid id, ProductForUpdateDto updateDto)
        {
            bool result = true;
            LogInfo(nameof(UpdateFully), LogMessages.MessageForStartingMethodExecution);
            Product? product = _repository.Product.GetById(isTrackChanges: true, id);
            if (product != null)
            {
                _mapService.Execute<ProductForUpdateDto, Product>(updateDto, product);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(nameof(UpdateFully), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Product), id.ToString()));
                result = false;
            }
            LogInfo(nameof(UpdateFully), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForStartingMethodExecution);
            var resultCheckList = _repository.Product.CheckRequiredConditionsForDeletion(id);
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
                    LogInfo(nameof(DeleteSoftly), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Product), id.ToString()));
                    result = false;
                }
            }
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            bool result = true;
            LogInfo(nameof(DeleteHard), LogMessages.MessageForStartingMethodExecution);
            var resultCheckList = _repository.Product.CheckRequiredConditionsForDeletion(id);
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
                    LogInfo(nameof(DeleteHard), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Product), id.ToString()));
                    result = false;
                }
            }
            LogInfo(nameof(DeleteHard), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

    }
}
