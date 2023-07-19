namespace eShopOnlineBusiness.Entities
{
    internal sealed class CompanyService : AbstractService, ICompanyService
    {
        public CompanyService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public CompanyDto? GetById(bool isTrackChanges, Guid id)
        {
            Company? company = _repository.Company.GetById(isTrackChanges, id);
            if (company == null)
            {
                return null;
            }
            return _mapperService.Execute<Company, CompanyDto>(company);
        }
    }
}
