﻿namespace Contracts.Business.Entities
{
    public interface IEmployeeService
    {
        EmployeeDto? GetById(Guid id);

        IEnumerable<EmployeeDto> GetAll();

        EmployeeDto Create(EmployeeForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
