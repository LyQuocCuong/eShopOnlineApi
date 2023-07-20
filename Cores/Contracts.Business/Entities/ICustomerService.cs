namespace Contracts.Business.Entities
{
    public interface ICustomerService
    {
        CustomerDto? GetById(Guid id);

        IEnumerable<CustomerDto> GetAll();

        CustomerDto Create(CustomerForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
