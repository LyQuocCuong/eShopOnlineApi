namespace eShopOnlineBusiness.Entities
{
    internal sealed class EmployeeService : AbstractService, IEmployeeService
    {
        public EmployeeService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<EmployeeDto> GetAll(bool isTrackChanges)
        {
            throw new NotImplementedException();
        }

        public EmployeeDto? GetById(bool isTrackChanges, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Create(EmployeeForCreationDto creationDto)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void HardDelete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
