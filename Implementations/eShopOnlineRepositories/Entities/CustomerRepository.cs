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

        public bool IsValidId(Guid id)
        {
            return base.FindByCondition(c => c.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<ConditionsForDeletingCustomer, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfCustomerDeletion);
        }

        public Dictionary<ConditionsForDeletingCustomer, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingCustomer> checkList)
        {
            Customer? customer = base.FindByCondition(c => c.Id == id, isTrackChanges: false).FirstOrDefault();
            var result = new Dictionary<ConditionsForDeletingCustomer, bool>();
            foreach (var condition in checkList)
            {
                result.Add(condition, false);
                if (customer != null)
                {
                    switch (condition)
                    {
                        case ConditionsForDeletingCustomer.IsNotDeletedSoftly:
                            if (customer.IsDeleted == false)
                            {
                                result[condition] = true;
                            }
                            break;
                    }

                }
            }
            return result;
        }

        public void Create(Customer customer)
        {
            base.CreateEntity(customer);
        }

        public void DeleteSoftly(Customer customer)
        {
            base.DeleteEntitySoftly(customer);
        }

        public void DeleteHard(Customer customer)
        {
            base.DeleteEntityHard(customer);
        }

    }
}
