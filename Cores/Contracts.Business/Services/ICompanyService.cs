namespace Contracts.Business.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<CompanyDto>> GetAllAsync();

        Task<bool> IsValidIdAsync(Guid id);

        Task<bool> UpdateFullyAsync(Guid id, CompanyForUpdateDto updateDto);
    }
}
