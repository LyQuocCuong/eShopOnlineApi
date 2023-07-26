namespace eShopOnlineRepositories.Entities
{
    internal sealed class EmployeeRepository : AbstractRepository<Employee>, IEmployeeRepository
    {
        protected override string ClassName => nameof(EmployeeRepository);

        public EmployeeRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Employee> GetAll(bool isTrackChanges)
        {
            LogMethodInfo(nameof(GetAll));
            return base.FindAll(isTrackChanges);
        }

        public Employee? GetById(bool isTrackChanges, Guid id)
        {
            LogMethodInfo(nameof(GetById));
            return base.FindByCondition(e => e.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));

            bool result = base.FindByCondition(e => e.Id == id, isTrackChanges: false).Any();

            LogMethodReturnInfo(result.ToString());
            return result;
        }

        public Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletingEmployee(Guid id)
        {
            LogMethodInfo(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletingEmployee)));
            return CheckRequiredConditionsForDeletingEmployee(id, DefaultDeleteEntityConditions.CheckListForDeletingAnEmployee);
        }

        public Dictionary<DeleteEmployeeCondition, bool> CheckRequiredConditionsForDeletingEmployee(Guid id, List<DeleteEmployeeCondition> checkList)
        {
            LogMethodInfo(nameof(CheckRequiredConditionsForDeletingEmployee));

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
                    LogInfo(RepositoryLogMessages.PassedCondition(prerequisiteCondition.ToString()));

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
                    LogInfo(RepositoryLogMessages.ObjectNotExistInDB(nameof(Employee), id));
                    LogInfo(RepositoryLogMessages.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                LogInfo(RepositoryLogMessages.MissingPrerequisiteCondition(ConditionsForDeletingEmployee.IsExistingInDatabase.ToString()));
            }
            return result;
        }

        public void Create(Employee employee)
        {
            LogMethodInfo(nameof(Create));
            base.CreateEntity(employee);
        }

        public void DeleteSoftly(Employee employee)
        {
            LogMethodInfo(nameof(DeleteSoftly));
            base.DeleteEntitySoftly(employee);
        }

        public void DeleteHard(Employee employee)
        {
            LogMethodInfo(nameof(DeleteHard));
            base.DeleteEntityHard(employee);
        }
    }
}
