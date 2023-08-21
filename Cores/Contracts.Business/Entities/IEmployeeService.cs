namespace Contracts.Business.Entities
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<EmployeeDto>> GetAllAsync();

        Task<bool> IsValidIdAsync(Guid id);

        Task<EmployeeDto> CreateAsync(EmployeeForCreationDto creationDto);

        Task<bool> UpdateFullyAsync(Guid id, EmployeeForUpdateDto updateDto);

        Task<bool> DeleteSoftlyAsync(Guid id);

        Task<bool> DeleteHardAsync(Guid id);
    }
}
