namespace Contracts.Repositories.Entities
{
    public interface IEmployeeRepository
    {
        Employee GetById(Guid id);

        IEnumerable<Employee> GetAll();

        void Create(Employee employee);

        void Update(Employee employee);

        void Delete(Guid id);
    }
}
