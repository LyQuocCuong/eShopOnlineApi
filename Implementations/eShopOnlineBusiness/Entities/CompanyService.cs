namespace eShopOnlineBusiness.Entities
{
    internal sealed class CompanyService : AbstractService, ICompanyService
    {
        public CompanyService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            IEnumerable<Company> companies = _repository.Company.GetAll(isTrackChanges: false);
            return _mapperService.Execute<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }

        public CompanyDto? GetById(Guid id)
        {
            Company? company = _repository.Company.GetById(isTrackChanges: false, id);
            if (company == null)
            {
                return null;
            }
            return _mapperService.Execute<Company, CompanyDto>(company);
        }
    }
}
