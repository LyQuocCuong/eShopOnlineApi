namespace Contracts.Repositories.Entities
{
    public interface ICompanyRepository
    {
        Company GetById(Guid id);

        void Update(Company company);
    }
}
