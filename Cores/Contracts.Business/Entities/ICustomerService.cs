namespace Contracts.Business.Entities
{
    public interface ICustomerService
    {
        CustomerDto? GetById(Guid id);

        IEnumerable<CustomerDto> GetAll();

        bool IsValidId(Guid id);

        CustomerDto Create(CustomerForCreationDto creationDto);

        bool UpdateFully(Guid id, CustomerForUpdateDto updateDto);

        bool DeleteSoftly(Guid id);

        bool DeleteHard(Guid id);
    }
}
