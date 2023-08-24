namespace eShopOnlineRepositories.Entities
{
    public sealed class CustomerRepository : AbstractRepository<Customer, CustomerRepository>, ICustomerRepository
    {
        internal CustomerRepository(ILogger<CustomerRepository> logger, 
                                  RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(bool isTrackChanges)
        {
            return await base.FindAll(isTrackChanges).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(bool isTrackChanges, Guid id)
        {
            return await base.FindByCondition(c => c.Id == id, isTrackChanges).FirstOrDefaultAsync();
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await base.FindByCondition(c => c.Id == id, isTrackChanges: false).AnyAsync();
        }

        //public async Task<Dictionary<DeleteCustomerCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id)
        //{
        //    _logger.LogInformation(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletionAsync)));
        //    return await CheckRequiredConditionsForDeletionAsync(id, DefaultDeleteEntityConditions.CheckListForDeletingACustomer);
        //}

        //public async Task<Dictionary<DeleteCustomerCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteCustomerCondition> checkList)
        //{
        //    var result = new Dictionary<DeleteCustomerCondition, bool>();
            
        //    DeleteCustomerCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingCustomer.IsExistingInDatabase);
        //    if (prerequisiteCondition != null)
        //    {
        //        result.Add(prerequisiteCondition, false);
        //        checkList.Remove(prerequisiteCondition);

        //        Customer? customer = await GetByIdAsync(isTrackChanges: false, id);
        //        if (customer != null)
        //        {
        //            result[prerequisiteCondition] = true;
        //            _logger.LogInformation(RepositoryLogs.PassedCondition(prerequisiteCondition.ToString()));

        //            // checking Other Conditions
        //            foreach (var item in checkList)
        //            {
        //                result.Add(item, false);
        //                switch (item.Condition)
        //                {
        //                    case ConditionsForDeletingCustomer.IsNotDeletedSoftly:
        //                        if (customer.IsDeleted == false)
        //                        {
        //                            result[item] = true;
        //                        }
        //                        break;
        //                    default:
        //                        _logger.LogWarning(RepositoryLogs.NotImplementedCondition(item.ToString()));
        //                        break;
        //                }
        //                if (result[item])
        //                {
        //                    _logger.LogInformation(RepositoryLogs.PassedCondition(item.ToString()));
        //                }
        //                else
        //                {
        //                    _logger.LogWarning(RepositoryLogs.FailedCondition(item.ToString()));
        //                    break;  // stop checking
        //                }
        //            }
        //        }
        //        else
        //        {
        //            _logger.LogWarning(RepositoryLogs.ObjectNotExistInDB(nameof(Customer), id));
        //            _logger.LogWarning(RepositoryLogs.FailedCondition(prerequisiteCondition.ToString()));
        //        }
        //    }
        //    else
        //    {
        //        _logger.LogError(RepositoryLogs.MissingPrerequisiteCondition(ConditionsForDeletingCustomer.IsExistingInDatabase.ToString()));
        //    }
        //    return result;
        //}

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
