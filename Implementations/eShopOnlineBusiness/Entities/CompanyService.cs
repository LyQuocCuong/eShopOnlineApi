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

        public bool IsValidId(Guid id)
        {
            return _repository.Company.IsValidId(id);
        }

        public bool UpdateFully(Guid id, CompanyForUpdateDto updateDto)
        {
            Company? company = _repository.Company.GetById(isTrackChanges: true, id);
            if (company == null)
            {
                throw new Exception();
            }
            _mapperService.Execute<CompanyForUpdateDto, Company>(updateDto, company);
            _repository.SaveChanges();
            return true;
        }

    }
}
