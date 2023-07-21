namespace Contracts.Repositories.Entities
{
    public interface IEmployeeRepository
    {
        Employee? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Employee> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<ConditionsForDeletingEmployee, bool> CheckRequiredConditionsForDeletion(Guid id);

        Dictionary<ConditionsForDeletingEmployee, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingEmployee> checkList);

        void Create(Employee employee);

        void DeleteSoftly(Employee employee);

        void DeleteHard(Employee employee);
    }
}
