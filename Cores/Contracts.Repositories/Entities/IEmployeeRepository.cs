namespace Contracts.Repositories.Entities
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(bool isTrackChanges, Guid id);

        Task<IEnumerable<Employee>> GetAllAsync(bool isTrackChanges);

        Task<bool> IsValidIdAsync(Guid id);

        //Task<Dictionary<DeleteEmployeeCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id);

        //Task<Dictionary<DeleteEmployeeCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteEmployeeCondition> checkList);

        void Create(Employee employee);

        void DeleteSoftly(Employee employee);

        void DeleteHard(Employee employee);
    }
}
