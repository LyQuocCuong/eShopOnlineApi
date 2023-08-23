﻿namespace eShopOnlineBusiness.Entities
{
    public sealed class EmployeeService : AbstractService<EmployeeService>, IEmployeeService
    {
        public EmployeeService(ILogger<EmployeeService> logger, 
                                 ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            IEnumerable<Employee> employees = await _repository.Employee.GetAllAsync(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            Employee? employee = await _repository.Employee.GetByIdAsync(isTrackChanges: false, id);
            if (employee == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                return null;
            }
            return _mapService.Execute<Employee, EmployeeDto>(employee);
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await _repository.Employee.IsValidIdAsync(id);
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeForCreationDto creationDto)
        {
            Employee newEmployee = _mapService.Execute<EmployeeForCreationDto, Employee>(creationDto);
            _repository.Employee.Create(newEmployee);
            await _repository.SaveChangesAsync();

            return _mapService.Execute<Employee, EmployeeDto>(newEmployee);
        }

        public async Task<bool> UpdateFullyAsync(Guid id, EmployeeForUpdateDto updateDto)
        {
            bool result = true;
            Employee? employee = await _repository.Employee.GetByIdAsync(isTrackChanges: true, id);
            if (employee != null)
            {
                _mapService.Execute<EmployeeForUpdateDto, Employee>(updateDto, employee);
                await _repository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                result = false;
            }
            return result;
        }

        public async Task<bool> DeleteSoftlyAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await _repository.Employee.CheckRequiredConditionsForDeletionAsync(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Employee? employee = await _repository.Employee.GetByIdAsync(isTrackChanges: true, id);
                if (employee != null)
                {
                    _repository.Employee.DeleteSoftly(employee);
                    await _repository.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Employee), id));
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> DeleteHardAsync(Guid id)
        {
            bool result = true;
            var resultCheckList = await _repository.Employee.CheckRequiredConditionsForDeletionAsync(id);
            if (resultCheckList.Any(condition => condition.Value == false))
            {
                result = false;
            }
            else
            {
                Employee? employee = await _repository.Employee.GetByIdAsync(isTrackChanges: true, id);
                if (employee != null)
                {
                    _repository.Employee.DeleteHard(employee);
                    await _repository.SaveChangesAsync();
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
