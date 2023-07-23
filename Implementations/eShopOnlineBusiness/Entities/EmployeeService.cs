namespace eShopOnlineBusiness.Entities
{
    internal sealed class EmployeeService : AbstractService, IEmployeeService
    {
        protected override string ChildClassName => nameof(EmployeeService);

        public EmployeeService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            IEnumerable<Employee> employees = _repository.Employee.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetById(Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            Employee? employee = _repository.Employee.GetById(isTrackChanges: false, id);
            if (employee == null)
            {
                LogInfo(nameof(GetById), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Employee), id.ToString()));
                return null;
            }
            return _mapperService.Execute<Employee, EmployeeDto>(employee);
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return _repository.Employee.IsValidId(id);
        }

        public EmployeeDto Create(EmployeeForCreationDto creationDto)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);

            Employee newEmployee = _mapperService.Execute<EmployeeForCreationDto, Employee>(creationDto);
            _repository.Employee.Create(newEmployee);
            _repository.SaveChanges();

            return _mapperService.Execute<Employee, EmployeeDto>(newEmployee);
        }

        public bool UpdateFully(Guid id, EmployeeForUpdateDto updateDto)
        {
            bool result = true;
            LogInfo(nameof(UpdateFully), LogMessages.MessageForStartingMethodExecution);
            Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
            if (employee != null)
            {
                _mapperService.Execute<EmployeeForUpdateDto, Employee>(updateDto, employee);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(nameof(UpdateFully), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Employee), id.ToString()));
                result = false;
            }
            LogInfo(nameof(UpdateFully), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForStartingMethodExecution);
            var resultCheckList = _repository.Employee.CheckRequiredConditionsForDeletion(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
                if (employee != null)
                {
                    _repository.Employee.DeleteSoftly(employee);
                    _repository.SaveChanges();
                }
                else
                {
                    LogInfo(nameof(DeleteSoftly), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Employee), id.ToString()));
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
            var resultCheckList = _repository.Employee.CheckRequiredConditionsForDeletion(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
                if (employee != null)
                {
                    _repository.Employee.DeleteHard(employee);
                    _repository.SaveChanges();
                    result = true;
                }
                else
                {
                    LogInfo(nameof(DeleteHard), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Employee), id.ToString()));
                    result = false;
                }
            }
            LogInfo(nameof(DeleteHard), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

    }
}
