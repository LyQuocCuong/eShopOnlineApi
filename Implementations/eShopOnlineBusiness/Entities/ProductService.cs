﻿namespace eShopOnlineBusiness.Entities
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

        public bool IsValidId(Guid id)
        {
            return _repository.Product.IsValidId(id);
        }

        public ProductDto Create(ProductForCreationDto creationDto)
        {
            Product newProduct = _mapperService.Execute<ProductForCreationDto, Product>(creationDto);
            _repository.Product.Create(newProduct);
            _repository.SaveChanges();

            return _mapperService.Execute<Product, ProductDto>(newProduct);
        }

        public bool UpdateFully(Guid id, ProductForUpdateDto updateDto)
        {
            Product? product = _repository.Product.GetById(isTrackChanges: true, id);
            if (product == null)
            {
                throw new Exception();
            }
            _mapperService.Execute<ProductForUpdateDto, Product>(updateDto, product);
            _repository.SaveChanges();
            return true;
        }

        public bool DeleteSoftly(Guid id)
        {
            var result = _repository.Product.CheckRequiredConditionsForDeletion(id);
            if (result.Any(condition => condition.Value == false))
            {
                return false;
            }
            Product? product = _repository.Product.GetById(isTrackChanges: true, id);
            if (product != null)
            {
                _repository.Product.DeleteSoftly(product);
                _repository.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteHard(Guid id)
        {
            var result = _repository.Product.CheckRequiredConditionsForDeletion(id);
            if (result.Any(condition => condition.Value == false))
            {
                return false;
            }
            Product? product = _repository.Product.GetById(isTrackChanges: true, id);
            if (product != null)
            {
                _repository.Product.DeleteHard(product);
                _repository.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
