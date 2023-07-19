namespace Contracts.Business.Entities
{
    public interface IEmployeeService
    {
        EmployeeDto? GetById(bool isTrackChanges, Guid id);

        IEnumerable<EmployeeDto> GetAll(bool isTrackChanges);

        void Create(EmployeeForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
