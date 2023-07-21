namespace eShopOnlineRepositories.Entities
{
    internal sealed class EmployeeRepository : AbstractRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryParams repositoryParams) : base(repositoryParams)
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
            return base.FindByCondition(e => e.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<ConditionsForDeletingEmployee, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfEmployeeDeletion);
        }

        public Dictionary<ConditionsForDeletingEmployee, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingEmployee> checkList)
        {
            Employee? employee = base.FindByCondition(e => e.Id == id, isTrackChanges : false).FirstOrDefault();
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
                                result[condition] = true;
                            }
                            break;
                        case ConditionsForDeletingEmployee.IsNotAdminRoot:
                            if (employee.Id != SeedingEntities.ROOT_ADMIN.Id)
                            {
                                result[condition] = true;
                            }
                            break;
                        case ConditionsForDeletingEmployee.IsNotManagerOfStore:
                            if (employee.ManagingStore == null)
                            {
                                result[condition] = true;
                            }
                            break;
                    }
                }
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
