namespace Contracts.Repositories.Entities
{
    public interface IEmployeeRepository
    {
        Employee? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Employee> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletion(Guid id);

        Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletion(Guid id, List<DeleteEmployeeCondition> checkList);

        void Create(Employee employee);

        void DeleteSoftly(Employee employee);

        void DeleteHard(Employee employee);
    }
}
