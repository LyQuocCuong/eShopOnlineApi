namespace Contracts.Business.Entities
{
    public interface ICompanyService
    {
        CompanyDto? GetById(Guid id);

        IEnumerable<CompanyDto> GetAll();

        bool IsValidId(Guid id);

        bool UpdateFully(Guid id, CompanyForUpdateDto updateDto);
    }
}
