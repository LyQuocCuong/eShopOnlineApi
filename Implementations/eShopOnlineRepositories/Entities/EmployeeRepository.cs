namespace eShopOnlineRepositories.Entities
{
    public sealed class EmployeeRepository : AbstractRepository<Employee, EmployeeRepository>, IEmployeeRepository
    {
        internal EmployeeRepository(ILogger<EmployeeRepository> logger, 
                                  RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
        {
        }

        public IEnumerable<Employee> GetAll(bool isTrackChanges)
        {
            return base.FindAll(isTrackChanges);
        }

        public Employee? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(e => e.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            bool result = base.FindByCondition(e => e.Id == id, isTrackChanges: false).Any();
            return result;
        }

        public Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletingEmployee(Guid id)
        {
            _logger.LogInformation(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletingEmployee)));
            return CheckRequiredConditionsForDeletingEmployee(id, DefaultDeleteEntityConditions.CheckListForDeletingAnEmployee);
        }

        public Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletingEmployee(Guid id, List<DeleteEmployeeCondition> checkList)
        {
            var result = new Dictionary<DeleteEmployeeCondition, bool>();

            DeleteEmployeeCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingEmployee.IsExistingInDatabase);
            if (prerequisiteCondition != null)
            {
                result.Add(prerequisiteCondition, false);
                checkList.Remove(prerequisiteCondition);

                Employee? employee = base.FindByCondition(e => e.Id == id, isTrackChanges: false).FirstOrDefault();
                if (employee != null)
                {
                    result[prerequisiteCondition] = true;
                    _logger.LogInformation(RepositoryLogs.PassedCondition(prerequisiteCondition.ToString()));

                    // checking Other Conditions
                    foreach (var item in checkList)
                    {
                        result.Add(item, false);
                        switch (item.Condition)
                        {
                            case ConditionsForDeletingEmployee.IsNotDeletedSoftly:
                                if (employee.IsDeleted == false)
                                {
                                    result[item] = true;
                                }
                                break;
                            case ConditionsForDeletingEmployee.IsNotAdminRoot:
                                if (employee.Id != SeedingEntities.ROOT_ADMIN.Id)
                                {
                                    result[item] = true;
                                }
                                break;
                            case ConditionsForDeletingEmployee.IsNotManagerOfStore:
                                if (employee.ManagingStore == null)
                                {
                                    result[item] = true;
                                }
                                break;
                            default:
                                _logger.LogWarning(RepositoryLogs.NotImplementedCondition(item.ToString()));
                                break;
                        }
                        if (result[item])
                        {
                            _logger.LogInformation(RepositoryLogs.PassedCondition(item.ToString()));
                        }
                        else
                        {
                            _logger.LogWarning(RepositoryLogs.FailedCondition(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    _logger.LogWarning(RepositoryLogs.ObjectNotExistInDB(nameof(Employee), id));
                    _logger.LogWarning(RepositoryLogs.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                _logger.LogError(RepositoryLogs.MissingPrerequisiteCondition(ConditionsForDeletingEmployee.IsExistingInDatabase.ToString()));
            }
            return result;
        }

        public void Create(Employee employee)
        {
            base.CreateEntity(employee);
        }

        public void DeleteSoftly(Employee employee)
        {
            base.DeleteEntitySoftly(employee);
        }

        public void DeleteHard(Employee employee)
        {
            base.DeleteEntityHard(employee);
        }
    }
}
