namespace eShopOnlineBusiness.Entities
{
    internal sealed class CustomerService : AbstractService, ICustomerService
    {
        protected override string ChildClassName => nameof(CustomerService);

        public CustomerService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            IEnumerable<Customer> customers = _repository.Customer.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return _repository.Customer.IsValidId(id);
        }

        public CustomerDto? GetById(Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            Customer? customer = _repository.Customer.GetById(isTrackChanges: false, id);
            if (customer == null)
            {
                LogInfo(nameof(GetById), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Customer), id.ToString()));
                return null;
            }
            return _mapperService.Execute<Customer, CustomerDto>(customer);
        }

        public CustomerDto Create(CustomerForCreationDto creationDto)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);

            Customer newCustomer = _mapperService.Execute<CustomerForCreationDto, Customer>(creationDto);
            _repository.Customer.Create(newCustomer);
            _repository.SaveChanges();

            return _mapperService.Execute<Customer, CustomerDto>(newCustomer);
        }

        public bool UpdateFully(Guid id, CustomerForUpdateDto updateDto)
        {
            bool result = true;
            LogInfo(nameof(UpdateFully), LogMessages.MessageForStartingMethodExecution);
            Customer? customer = _repository.Customer.GetById(isTrackChanges: true, id);
            if (customer != null)
            {
                _mapperService.Execute<CustomerForUpdateDto, Customer>(updateDto, customer);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(nameof(UpdateFully), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Customer), id.ToString()));
                result = false;
            }            
            LogInfo(nameof(UpdateFully), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForStartingMethodExecution);
            var resultCheckList = _repository.Customer.CheckRequiredConditionsForDeletion(id);
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
                    LogInfo(nameof(DeleteSoftly), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Customer), id.ToString()));
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
            var resultCheckList = _repository.Customer.CheckRequiredConditionsForDeletion(id);
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
                    LogInfo(nameof(DeleteHard), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Customer), id.ToString()));
                    result = false;
                }
            }
            LogInfo(nameof(DeleteHard), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

    }
}
