﻿namespace eShopOnlineBusiness.Services
{
    public sealed class ProductService : AbstractService<ProductService>, IProductService
    {
        internal ProductService(ILogger<ProductService> logger, 
                                ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            IEnumerable<Product> products = await _repository.Product.GetAllAsync(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            Product? product = await _repository.Product.GetByIdAsync(isTrackChanges: false, id);
            if(product == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Product), id));
                return null;
            }
            return _mapService.Execute<Product, ProductDto>(product);
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await _repository.Product.IsValidIdAsync(id);
        }

        public async Task<ProductDto> CreateAsync(ProductForCreationDto creationDto)
        {
            Product newProduct = _mapService.Execute<ProductForCreationDto, Product>(creationDto);
            _repository.Product.Create(newProduct);
            await _repository.SaveChangesAsync();

            return _mapService.Execute<Product, ProductDto>(newProduct);
        }

        public async Task<bool> UpdateFullyAsync(Guid id, ProductForUpdateDto updateDto)
        {
            bool result = true;
            Product? product = await _repository.Product.GetByIdAsync(isTrackChanges: true, id);
            if (product != null)
            {
                _mapService.Execute<ProductForUpdateDto, Product>(updateDto, product);
                await _repository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Product), id));
                result = false;
            }
            return result;
        }

        private async Task<Dictionary<DeleteProductCondition, bool>> CheckConditionsForDeletingAProductAsync(Guid id, List<DeleteProductCondition> checkList)
        {
            var result = new Dictionary<DeleteProductCondition, bool>()
            {
                { DeleteProductConditionDictionary.IsExistingInDatabase, false },
            };

            Product? Product = await _repository.Product.GetByIdAsync(isTrackChanges: false, id);
            if (Product == null)
            {
                return result;   // stop
            }

            result[DeleteProductConditionDictionary.IsExistingInDatabase] = true;
            checkList.Remove(DeleteProductConditionDictionary.IsExistingInDatabase);

            foreach (var item in checkList)
            {
                result.Add(item, false);
                switch (item.Condition)
                {
                    case DeleteProductConditionsEnum.IsNotDeletedSoftly:
                        if (Product.IsDeleted == false)
                        {
                            result[item] = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public async Task<bool> DeleteSoftlyAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await CheckConditionsForDeletingAProductAsync(id, DefaultRequiredConditions.DeleteAProduct);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Product? product = await _repository.Product.GetByIdAsync(isTrackChanges: true, id);
                if (product != null)
                {
                    _repository.Product.DeleteSoftly(product);
                    await _repository.SaveChangesAsync();
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

        public async Task<bool> DeleteHardAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await CheckConditionsForDeletingAProductAsync(id, DefaultRequiredConditions.DeleteAProduct);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                return false;
            }
            else
            {
                Product? product = await _repository.Product.GetByIdAsync(isTrackChanges: true, id);
                if (product != null)
                {
                    _repository.Product.DeleteHard(product);
                    await _repository.SaveChangesAsync();
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
