﻿namespace eShopOnlineBusiness.Entities
{
    internal sealed class CustomerService : AbstractService, ICustomerService
    {
        public CustomerService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CustomerDto> GetAll(bool isTrackChanges)
        {
            IEnumerable<Customer> customers = _repositoryManager.Customer.GetAll(isTrackChanges);
            return _mapperService.Execute<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
        }

        public CustomerDto? GetById(bool isTrackChanges, Guid id)
        {
            Customer? customer = _repositoryManager.Customer.GetById(isTrackChanges, id);
            if (customer == null)
            {
                return null;
            }
            return _mapperService.Execute<Customer, CustomerDto>(customer);
        }

        public void Create(CustomerForCreationDto creationDto)
        {
            Customer newCustomer = _mapperService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repositoryManager.Customer.Create(newCustomer);
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