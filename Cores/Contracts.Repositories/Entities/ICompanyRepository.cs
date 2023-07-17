namespace Contracts.Repositories.Entities
{
    public interface ICompanyRepository
    {
        Company? GetById(bool isTrackChanges, Guid id);
    }
}
