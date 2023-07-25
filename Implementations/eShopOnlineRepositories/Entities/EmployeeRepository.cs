namespace eShopOnlineRepositories.Entities
{
    internal sealed class EmployeeRepository : AbstractRepository<Employee>, IEmployeeRepository
    {
        protected override string ChildClassName => nameof(EmployeeRepository);

        public EmployeeRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Employee> GetAll(bool isTrackChanges)
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            return base.FindAll(isTrackChanges);
        }

        public Employee? GetById(bool isTrackChanges, Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(e => e.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(e => e.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, DefaultDeleteEntityConditions.CheckListForDeletingAnEmployee);
        }

        public Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletion(Guid id, List<DeleteEmployeeCondition> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingMethod);

            var result = new Dictionary<DeleteEmployeeCondition, bool>();

            Employee? employee = base.FindByCondition(e => e.Id == id, isTrackChanges : false).FirstOrDefault();
            DeleteEmployeeCondition? isExistingInDatabaseCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingEmployee.IsExistingInDatabase);
            if (isExistingInDatabaseCondition != null)
            {
                result.Add(isExistingInDatabaseCondition, false);
                checkList.Remove(isExistingInDatabaseCondition);
                if (employee != null)
                {
                    result[isExistingInDatabaseCondition] = true;
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(isExistingInDatabaseCondition.ToString()));

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
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Employee), id.ToString()));
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(isExistingInDatabaseCondition.ToString()));
                }
            }
            else
            {
                LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed("Missing IsExistingInDatabase."));
            }
            return result;
        }

        public void Create(Employee employee)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);
            base.CreateEntity(employee);
        }

        public void DeleteSoftly(Employee employee)
        {
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForExecutingMethod);
            base.DeleteEntitySoftly(employee);
        }

        public void DeleteHard(Employee employee)
        {
            LogInfo(nameof(DeleteHard), LogMessages.MessageForExecutingMethod);
            base.DeleteEntityHard(employee);
        }
    }
}
