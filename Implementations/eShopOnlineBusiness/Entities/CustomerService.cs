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

        public CustomerDto? GetById(Guid id)
        {
            Customer? customer = _repository.Customer.GetById(isTrackChanges: false, id);
            if (customer == null)
            {
                return null;
            }
            return _mapperService.Execute<Customer, CustomerDto>(customer);
        }

        public void Create(CustomerForCreationDto creationDto)
        {
            Customer newCustomer = _mapperService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repository.Customer.Create(newCustomer);
            _repository.SaveChanges();
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
