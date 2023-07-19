namespace Contracts.Business.Entities
{
    public interface ICompanyService
    {
        CompanyDto? GetById(bool isTrackChanges, Guid id);
    }
}
