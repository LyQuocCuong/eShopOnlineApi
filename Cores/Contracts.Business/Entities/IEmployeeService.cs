namespace Contracts.Business.Entities
{
    public interface IEmployeeService
    {
        EmployeeDto? GetById(Guid id);

        IEnumerable<EmployeeDto> GetAll();

        bool IsValidId(Guid id);

        EmployeeDto Create(EmployeeForCreationDto creationDto);

        bool UpdateFully(Guid id, EmployeeForUpdateDto updateDto);

        bool DeleteSoftly(Guid id);

        bool DeleteHard(Guid id);
    }
}
