namespace eShopOnlineBusiness.Entities
{
    public sealed class CustomerService : AbstractService<CustomerService>, ICustomerService
    {
        internal CustomerService(ILogger<CustomerService> logger, 
                                 ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            IEnumerable<Customer> customers = _repository.Customer.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
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
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                return null;
            }
            return _mapService.Execute<Customer, CustomerDto>(customer);
        }

        public CustomerDto Create(CustomerForCreationDto creationDto)
        {
            Customer newCustomer = _mapService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repository.Customer.Create(newCustomer);
            _repository.SaveChanges();

            return _mapService.Execute<Customer, CustomerDto>(newCustomer);
        }

        public bool UpdateFully(Guid id, CustomerForUpdateDto updateDto)
        {
            bool result = true;
            Customer? customer = _repository.Customer.GetById(isTrackChanges: true, id);
            if (customer != null)
            {
                _mapService.Execute<CustomerForUpdateDto, Customer>(updateDto, customer);
                _repository.SaveChanges();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                result = false;
            }
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Customer.CheckRequiredConditionsForDeletingCustomer(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Customer? customer = _repository.Customer.GetById(isTrackChanges: true, id);
                if (customer != null)
                {
                    _repository.Customer.DeleteSoftly(customer);
                    _repository.SaveChanges();
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Customer.CheckRequiredConditionsForDeletingCustomer(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Customer? customer = _repository.Customer.GetById(isTrackChanges: true, id);
                if (customer != null)
                {
                    _repository.Customer.DeleteHard(customer);
                    _repository.SaveChanges();
                    result = true;
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                    result = false;
                }
            }
            return result;
        }

    }
}
