namespace eShopOnlineBusiness.Entities
{
    public sealed class CompanyService : AbstractService<CompanyService>, ICompanyService
    {
        internal CompanyService(ILogger<CompanyService> logger, 
                              ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            IEnumerable<Company> companies = _repository.Company.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }

        public CompanyDto? GetById(Guid id)
        {
            Company? company = _repository.Company.GetById(isTrackChanges: false, id);
            if (company == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Company), id));
                return null;
            }
            return _mapService.Execute<Company, CompanyDto>(company);
        }

        public bool IsValidId(Guid id)
        {
            return _repository.Company.IsValidId(id);
        }

        public bool UpdateFully(Guid id, CompanyForUpdateDto updateDto)
        {
            bool result = true;
            Company? company = _repository.Company.GetById(isTrackChanges: true, id);
            if (company != null)
            {
                _mapService.Execute<CompanyForUpdateDto, Company>(updateDto, company);
                _repository.SaveChanges();
            }
            else
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Company), id));
                result = false;                
            }
            return result;
        }

    }
}
