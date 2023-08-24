namespace Contracts.Repositories.Entities
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(bool isTrackChanges, Guid id);

        Task<IEnumerable<Customer>> GetAllAsync(bool isTrackChanges);

        Task<bool> IsValidIdAsync(Guid id);

        //Task<Dictionary<DeleteCustomerCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id);

        //Task<Dictionary<DeleteCustomerCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteCustomerCondition> checkList);

        void Create(Customer customer);

        void DeleteSoftly(Customer customer);

        void DeleteHard(Customer customer);
    }
}
