namespace eShopOnlineRepositories.Entities
{
    internal sealed class CustomerRepository : AbstractRepository<Customer>, ICustomerRepository
    {
        protected override string ClassName => nameof(CustomerRepository);

        public CustomerRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Customer> GetAll(bool isTrackChanges)
        {
            LogMethodInfo(nameof(GetAll));
            return base.FindAll(isTrackChanges);
        }

        public Customer? GetById(bool isTrackChanges, Guid id)
        {
            LogMethodInfo(nameof(GetById));
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));
            
            bool result = base.FindByCondition(c => c.Id == id, isTrackChanges: false).Any();

            LogMethodReturnInfo(result.ToString());
            return result;
        }

        public Dictionary<DeleteCustomerCondition, bool> CheckRequiredConditionsForDeletingCustomer(Guid id)
        {
            LogMethodInfo(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletingCustomer)));
            return CheckRequiredConditionsForDeletingCustomer(id, DefaultDeleteEntityConditions.CheckListForDeletingACustomer);
        }

        public Dictionary<DeleteCustomerCondition, bool> CheckRequiredConditionsForDeletingCustomer(Guid id, List<DeleteCustomerCondition> checkList)
        {
            LogMethodInfo(nameof(CheckRequiredConditionsForDeletingCustomer));

            var result = new Dictionary<DeleteCustomerCondition, bool>();
            
            DeleteCustomerCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingCustomer.IsExistingInDatabase);
            if (prerequisiteCondition != null)
            {
                result.Add(prerequisiteCondition, false);
                checkList.Remove(prerequisiteCondition);

                Customer? customer = base.FindByCondition(c => c.Id == id, isTrackChanges: false).FirstOrDefault();
                if (customer != null)
                {
                    result[prerequisiteCondition] = true;
                    LogInfo(RepositoryLogMessages.PassedCondition(prerequisiteCondition.ToString()));

                    // checking Other Conditions
                    foreach (var item in checkList)
                    {
                        result.Add(item, false);
                        switch (item.Condition)
                        {
                            case ConditionsForDeletingCustomer.IsNotDeletedSoftly:
                                if (customer.IsDeleted == false)
                                {
                                    result[item] = true;
                                }
                                break;
                            default:
                                LogWarning(RepositoryLogMessages.NotImplementedCondition(item.ToString()));
                                break;
                        }
                        if (result[item])
                        {
                            LogInfo(RepositoryLogMessages.PassedCondition(item.ToString()));
                        }
                        else
                        {
                            LogInfo(RepositoryLogMessages.FailedCondition(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    LogInfo(RepositoryLogMessages.ObjectNotExistInDB(nameof(Customer), id));
                    LogInfo(RepositoryLogMessages.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                LogInfo(RepositoryLogMessages.MissingPrerequisiteCondition(ConditionsForDeletingCustomer.IsExistingInDatabase.ToString()));
            }
            return result;
        }

        public void Create(Customer customer)
        {
            LogMethodInfo(nameof(Create));
            base.CreateEntity(customer);
        }

        public void DeleteSoftly(Customer customer)
        {
            LogMethodInfo(nameof(DeleteSoftly));
            base.DeleteEntitySoftly(customer);
        }

        public void DeleteHard(Customer customer)
        {
            LogMethodInfo(nameof(DeleteHard));
            base.DeleteEntityHard(customer);
        }

    }
}
