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

        public EmployeeDto Create(EmployeeForCreationDto creationDto)
        {
            Employee newEmployee = _mapperService.Execute<EmployeeForCreationDto, Employee>(creationDto);
            _repository.Employee.Create(newEmployee);
            _repository.SaveChanges();

            return _mapperService.Execute<Employee, EmployeeDto>(newEmployee);
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
