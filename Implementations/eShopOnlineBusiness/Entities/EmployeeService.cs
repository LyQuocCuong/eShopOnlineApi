namespace eShopOnlineBusiness.Entities
{
    public sealed class EmployeeService : AbstractService<EmployeeService>, IEmployeeService
    {
        internal EmployeeService(ILogger<EmployeeService> logger, 
                                 ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            IEnumerable<Employee> employees = _repository.Employee.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetById(Guid id)
        {
            Employee? employee = _repository.Employee.GetById(isTrackChanges: false, id);
            if (employee == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                return null;
            }
            return _mapService.Execute<Employee, EmployeeDto>(employee);
        }

        public bool IsValidId(Guid id)
        {
            return _repository.Employee.IsValidId(id);
        }

        public EmployeeDto Create(EmployeeForCreationDto creationDto)
        {
            Employee newEmployee = _mapService.Execute<EmployeeForCreationDto, Employee>(creationDto);
            _repository.Employee.Create(newEmployee);
            _repository.SaveChanges();

            return _mapService.Execute<Employee, EmployeeDto>(newEmployee);
        }

        public bool UpdateFully(Guid id, EmployeeForUpdateDto updateDto)
        {
            bool result = true;
            Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
            if (employee != null)
            {
                _mapService.Execute<EmployeeForUpdateDto, Employee>(updateDto, employee);
                _repository.SaveChanges();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                result = false;
            }
            return result;
        }

        public bool DeleteSoftly(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Employee.CheckRequiredConditionsForDeletingEmployee(id);
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
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteHard(Guid id)
        {
            bool result = true;
            var resultCheckList = _repository.Employee.CheckRequiredConditionsForDeletingEmployee(id);
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
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                    result = false;
                }
            }
            return result;
        }

    }
}
