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

        public Dictionary<ConditionsForDeletingEmployee, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfEmployeeDeletion);
        }

        public Dictionary<ConditionsForDeletingEmployee, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingEmployee> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForStartingMethodExecution);
            Employee? employee = base.FindByCondition(e => e.Id == id, isTrackChanges : false).FirstOrDefault();
            if (employee == null)
            {
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Employee), id.ToString()));
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForSettingAllConditionsInCheckListToFalse);
            }
            var result = new Dictionary<ConditionsForDeletingEmployee, bool>();
            foreach (var condition in checkList)
            {
                result.Add(condition, false);
                if (employee != null)
                {
                    switch (condition)
                    {
                        case ConditionsForDeletingEmployee.IsNotDeletedSoftly:
                            if (employee.IsDeleted == false)
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - PASSED.");
                                result[condition] = true;
                            }
                            else
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the Employee has been deleted softly (IsDeleted = TRUE).");
                            }
                            break;
                        case ConditionsForDeletingEmployee.IsNotAdminRoot:
                            if (employee.Id != SeedingEntities.ROOT_ADMIN.Id)
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - PASSED.");
                                result[condition] = true;
                            }
                            else
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the Employee is the AdminRoot.");
                            }
                            break;
                        case ConditionsForDeletingEmployee.IsNotManagerOfStore:
                            if (employee.ManagingStore == null)
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - PASSED.");
                                result[condition] = true;
                            }
                            else
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the Employee is Manager of the Store.");
                            }
                            break;
                        default:
                            LogWarning(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the checking condition has NOT been implemented yet.");
                            break;
                    }
                }
            }
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForFinishingMethodExecution);
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
