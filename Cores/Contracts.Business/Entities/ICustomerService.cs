namespace Contracts.Business.Entities
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<bool> IsValidIdAsync(Guid id);

        Task<CustomerDto> CreateAsync(CustomerForCreationDto creationDto);

        Task<bool> UpdateFullyAsync(Guid id, CustomerForUpdateDto updateDto);

        Task<bool> DeleteSoftlyAsync(Guid id);

        Task<bool> DeleteHardAsync(Guid id);
    }
}
