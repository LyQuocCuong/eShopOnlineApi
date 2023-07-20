namespace Contracts.Repositories.Entities
{
    public interface ICustomerRepository
    {
        Customer? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Customer> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        void Create(Customer customer);

        void SoftDelete(Customer customer);

        void HardDelete(Customer customer);
    }
}
