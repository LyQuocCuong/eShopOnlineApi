namespace Contracts.Repositories.Entities
{
    public interface ICustomerRepository
    {
        Customer? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Customer> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<DeleteCustomerCondition, bool> CheckRequiredConditionsForDeletingCustomer(Guid id);

        Dictionary<DeleteCustomerCondition, bool> CheckRequiredConditionsForDeletingCustomer(Guid id, List<DeleteCustomerCondition> checkList);

        void Create(Customer customer);

        void DeleteSoftly(Customer customer);

        void DeleteHard(Customer customer);
    }
}
