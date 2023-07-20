namespace eShopOnlineBusiness.Entities
{
    internal sealed class CustomerService : AbstractService, ICustomerService
    {
        public CustomerService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            IEnumerable<Customer> customers = _repository.Customer.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
        }

        public bool IsValidId(Guid id)
        {
            return _repository.Customer.IsValidId(id);
        }

        public CustomerDto? GetById(Guid id)
        {
            Customer? customer = _repository.Customer.GetById(isTrackChanges: false, id);
            if (customer == null)
            {
                return null;
            }
            return _mapperService.Execute<Customer, CustomerDto>(customer);
        }

        public CustomerDto Create(CustomerForCreationDto creationDto)
        {
            Customer newCustomer = _mapperService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repository.Customer.Create(newCustomer);
            _repository.SaveChanges();

            return _mapperService.Execute<Customer, CustomerDto>(newCustomer);
        }

        public bool UpdateFully(Guid id, CustomerForUpdateDto updateDto)
        {
            Customer? customer = _repository.Customer.GetById(isTrackChanges: true, id);
            if (customer == null)
            {
                throw new Exception();
            }
            _mapperService.Execute<CustomerForUpdateDto, Customer>(updateDto, customer);
            _repository.SaveChanges();
            return true;
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
