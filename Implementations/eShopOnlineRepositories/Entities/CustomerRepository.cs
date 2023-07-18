namespace eShopOnlineRepositories.Entities
{
    internal sealed class CustomerRepository : AbstractRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Customer> GetAll(bool isTrackChanges)
        {
            return base.FindAll(isTrackChanges);
        }

        public Customer? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }

        public void Create(Customer customer)
        {
            base.CreateEntity(customer);
        }

        public void SoftDelete(Customer customer)
        {
            base.SoftDeleteEntity(customer);
        }

        public void HardDelete(Customer customer)
        {
            base.HardDeleteEntity(customer);
        }        
    }
}
