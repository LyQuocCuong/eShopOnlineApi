namespace eShopOnlineBusiness.Entities
{
    internal sealed class EmployeeService : AbstractService, IEmployeeService
    {
        public EmployeeService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            IEnumerable<Employee> employees = _repository.Employee.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetById(Guid id)
        {
            Employee? employee = _repository.Employee.GetById(isTrackChanges: false, id);
            if (employee == null)
            {
                return null;
            }
            return _mapperService.Execute<Employee, EmployeeDto>(employee);
        }

        public bool IsValidId(Guid id)
        {
            return _repository.Employee.IsValidId(id);
        }

        public EmployeeDto Create(EmployeeForCreationDto creationDto)
        {
            Employee newEmployee = _mapperService.Execute<EmployeeForCreationDto, Employee>(creationDto);
            _repository.Employee.Create(newEmployee);
            _repository.SaveChanges();

            return _mapperService.Execute<Employee, EmployeeDto>(newEmployee);
        }

        public bool UpdateFully(Guid id, EmployeeForUpdateDto updateDto)
        {
            Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
            if (employee == null)
            {
                throw new Exception();
            }
            _mapperService.Execute<EmployeeForUpdateDto, Employee>(updateDto, employee);
            _repository.SaveChanges();
            return true;
        }

        public bool DeleteSoftly(Guid id)
        {
            var result = _repository.Employee.CheckRequiredConditionsForDeletion(id);
            if (result.Any(condition => condition.Value == false))
            {
                return false;
            }
            Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
            if (employee != null)
            {
                _repository.Employee.DeleteSoftly(employee);
                _repository.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteHard(Guid id)
        {
            var result = _repository.Employee.CheckRequiredConditionsForDeletion(id);
            if (result.Any(condition => condition.Value == false))
            {
                return false;
            }
            Employee? employee = _repository.Employee.GetById(isTrackChanges: true, id);
            if (employee != null)
            {
                _repository.Employee.DeleteHard(employee);
                _repository.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
