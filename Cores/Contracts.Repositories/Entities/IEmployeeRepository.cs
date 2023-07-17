namespace Contracts.Repositories.Entities
{
    public interface IEmployeeRepository
    {
        Employee? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Employee> GetAll(bool isTrackChanges);

        void Create(Employee employee);

        void SoftDelete(Employee employee);

        void HardDelete(Employee employee);
    }
}
