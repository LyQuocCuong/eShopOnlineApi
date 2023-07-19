namespace Contracts.Business.Entities
{
    public interface ICompanyService
    {
        CompanyDto? GetById(Guid id);

        IEnumerable<CompanyDto> GetAll();
    }
}
