namespace eShopOnlineBusiness.Entities
{
    public sealed class CustomerService : AbstractService<CustomerService>, ICustomerService
    {
        internal CustomerService(ILogger<CustomerService> logger, 
                                 ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            IEnumerable<Customer> customers = await _repository.Customer.GetAllAsync(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await _repository.Customer.IsValidIdAsync(id);
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            Customer? customer = await _repository.Customer.GetByIdAsync(isTrackChanges: false, id);
            if (customer == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                return null;
            }
            return _mapService.Execute<Customer, CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CustomerForCreationDto creationDto)
        {
            Customer newCustomer = _mapService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repository.Customer.Create(newCustomer);
            await _repository.SaveChangesAsync();

            return _mapService.Execute<Customer, CustomerDto>(newCustomer);
        }

        public async Task<bool> UpdateFullyAsync(Guid id, CustomerForUpdateDto updateDto)
        {
            bool result = true;
            Customer? customer = await _repository.Customer.GetByIdAsync(isTrackChanges: true, id);
            if (customer != null)
            {
                _mapService.Execute<CustomerForUpdateDto, Customer>(updateDto, customer);
                await _repository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                result = false;
            }
            return result;
        }

        public async Task<bool> DeleteSoftlyAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await _repository.Customer.CheckRequiredConditionsForDeletionAsync(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Customer? customer = await _repository.Customer.GetByIdAsync(isTrackChanges: true, id);
                if (customer != null)
                {
                    _repository.Customer.DeleteSoftly(customer);
                    await _repository.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Customer), id));
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> DeleteHardAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await _repository.Customer.CheckRequiredConditionsForDeletionAsync(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Customer? customer = await _repository.Customer.GetByIdAsync(isTrackChanges: true, id);
                if (customer != null)
                {
                    _repository.Customer.DeleteHard(customer);
                    await _repository.SaveChangesAsync();
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
