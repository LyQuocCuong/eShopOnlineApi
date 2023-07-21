namespace Contracts.Repositories.Entities
{
    public interface ICustomerRepository
    {
        Customer? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Customer> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        Dictionary<ConditionsForDeletingCustomer, bool> CheckRequiredConditionsForDeletion(Guid id);

        Dictionary<ConditionsForDeletingCustomer, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingCustomer> checkList);

        void Create(Customer customer);

        void DeleteSoftly(Customer customer);

        void DeleteHard(Customer customer);
    }
}
