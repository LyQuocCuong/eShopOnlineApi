namespace Contracts.Business.Entities
{
    public interface IEmployeeService
    {
        EmployeeDto? GetById(Guid id);

        IEnumerable<EmployeeDto> GetAll();

        bool IsValidId(Guid id);

        EmployeeDto Create(EmployeeForCreationDto creationDto);

        bool UpdateFully(Guid id, EmployeeForUpdateDto updateDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
