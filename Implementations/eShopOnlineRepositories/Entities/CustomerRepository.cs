namespace eShopOnlineRepositories.Entities
{
    internal sealed class CustomerRepository : AbstractRepository<Customer>, ICustomerRepository
    {
        protected override string ChildClassName => nameof(CustomerRepository);

        public CustomerRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Customer> GetAll(bool isTrackChanges)
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            return base.FindAll(isTrackChanges);
        }

        public Customer? GetById(bool isTrackChanges, Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(c => c.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<DeleteCustomerCondition, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, DefaultDeleteEntityConditions.CheckListForDeletingACustomer);
        }

        public Dictionary<DeleteCustomerCondition, bool> CheckRequiredConditionsForDeletion(Guid id, List<DeleteCustomerCondition> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingMethod);

            var result = new Dictionary<DeleteCustomerCondition, bool>();
            
            Customer? customer = base.FindByCondition(c => c.Id == id, isTrackChanges: false).FirstOrDefault();
            DeleteCustomerCondition? isExistingInDatabaseCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingCustomer.IsExistingInDatabase);
            
            if (isExistingInDatabaseCondition != null)
            {
                result.Add(isExistingInDatabaseCondition, false);
                checkList.Remove(isExistingInDatabaseCondition);

                if (customer != null)
                {
                    result[isExistingInDatabaseCondition] = true;
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(isExistingInDatabaseCondition.ToString()));
                    
                    // checking Other Conditions
                    foreach (var item in checkList)
                    {
                        result.Add(item, false);
                        switch (item.Condition)
                        {
                            case ConditionsForDeletingCustomer.IsNotDeletedSoftly:
                                if (customer!.IsDeleted == false)
                                {
                                    result[item] = true;
                                }
                                break;
                            default:
                                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(LogMessages.MessageForNotImplementedCondition));
                                break;
                        }
                        if (result[item])
                        {
                            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(item.ToString()));
                        }
                        else
                        {
                            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Customer), id.ToString()));
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(isExistingInDatabaseCondition.ToString()));
                }
            }
            else
            {
                LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed("Missing IsExistingInDatabase Condition."));
            }
            return result;
        }

        public void Create(Customer customer)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);
            base.CreateEntity(customer);
        }

        public void DeleteSoftly(Customer customer)
        {
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForExecutingMethod);
            base.DeleteEntitySoftly(customer);
        }

        public void DeleteHard(Customer customer)
        {
            LogInfo(nameof(DeleteHard), LogMessages.MessageForExecutingMethod);
            base.DeleteEntityHard(customer);
        }

    }
}
