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

        public Dictionary<ConditionsForDeletingCustomer, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfCustomerDeletion);
        }

        public Dictionary<ConditionsForDeletingCustomer, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingCustomer> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForStartingMethodExecution);
            Customer? customer = base.FindByCondition(c => c.Id == id, isTrackChanges: false).FirstOrDefault();
            if (customer == null)
            {
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Customer), id.ToString()));
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForSettingAllConditionsInCheckListToFalse);
            }
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
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - PASSED. The Customer has not been deleted softly yet.");
                                result[condition] = true;
                            }
                            else
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the Customer has been deleted softly (IsDeleted = TRUE).");
                            }
                            break;
                        default:
                            LogWarning(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. The checking condition has NOT been implemented yet.");
                            break;
                    }

                }
            }
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForFinishingMethodExecution);
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
