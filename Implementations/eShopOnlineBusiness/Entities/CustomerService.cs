namespace eShopOnlineBusiness.Entities
{
    internal sealed class CustomerService : AbstractService, ICustomerService
    {
        protected override string ClassName => nameof(CustomerService);

        public CustomerService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            LogMethodInfo(nameof(GetAll));
            IEnumerable<Customer> customers = _repository.Customer.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));
            return _repository.Customer.IsValidId(id);
        }

        public CustomerDto? GetById(Guid id)
        {
            LogMethodInfo(nameof(GetById));

            Customer? customer = _repository.Customer.GetById(isTrackChanges: false, id);
            if (customer == null)
            {
                LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Customer), id));
                return null;
            }
            return _mapService.Execute<Customer, CustomerDto>(customer);
        }

        public CustomerDto Create(CustomerForCreationDto creationDto)
        {
            LogMethodInfo(nameof(Create));

            Customer newCustomer = _mapService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repository.Customer.Create(newCustomer);
            _repository.SaveChanges();

            return _mapService.Execute<Customer, CustomerDto>(newCustomer);
        }

        public bool UpdateFully(Guid id, CustomerForUpdateDto updateDto)
        {
            LogMethodInfo(nameof(UpdateFully));

            bool result = true;
            Customer? customer = _repository.Customer.GetById(isTrackChanges: true, id);
            if (customer != null)
            {
                _mapService.Execute<CustomerForUpdateDto, Customer>(updateDto, customer);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Customer), id));
                result = false;
            }
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            LogMethodInfo(nameof(DeleteSoftly));

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
                    LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Customer), id));
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            LogMethodInfo(nameof(DeleteHard));
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
                    LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Customer), id));
                    result = false;
                }
            }
            return result;
        }

    }
}
