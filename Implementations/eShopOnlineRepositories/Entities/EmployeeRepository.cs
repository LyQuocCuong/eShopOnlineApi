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

        public void Create(Employee employee)
        {
            base.CreateEntity(employee);
        }

        public void HardDelete(Employee employee)
        {
            base.HardDeleteEntity(employee);
        }

        public void SoftDelete(Employee employee)
        {
            base.SoftDeleteEntity(employee);
        }
    }
}
