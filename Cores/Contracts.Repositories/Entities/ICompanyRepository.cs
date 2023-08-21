namespace Contracts.Repositories.Entities
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(bool isTrackChanges, Guid id);

        Task<IEnumerable<Company>> GetAllAsync(bool isTrackChanges);

        Task<bool> IsValidIdAsync(Guid id);
    }
}
