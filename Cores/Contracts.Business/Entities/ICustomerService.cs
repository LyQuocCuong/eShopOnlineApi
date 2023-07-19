namespace Contracts.Business.Entities
{
    public interface ICustomerService
    {
        CustomerDto? GetById(bool isTrackChanges, Guid id);

        IEnumerable<CustomerDto> GetAll(bool isTrackChanges);

        void Create(CustomerForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
