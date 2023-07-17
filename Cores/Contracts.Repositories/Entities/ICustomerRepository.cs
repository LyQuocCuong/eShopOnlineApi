namespace Contracts.Repositories.Entities
{
    public interface ICustomerRepository
    {
        Customer GetById(Guid id);

        IEnumerable<Customer> GetAll();

        void Create(Customer customer);

        void Update(Customer customer);

        void Delete(Guid id);
    }
}
