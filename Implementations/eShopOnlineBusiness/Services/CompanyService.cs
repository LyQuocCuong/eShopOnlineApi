namespace eShopOnlineBusiness.Services
{
    public sealed class CompanyService : AbstractService<CompanyService>, ICompanyService
    {
        public CompanyService(ILogger<CompanyService> logger, 
                              ServiceParams serviceParams) 
            : base(logger, serviceParams)
        {
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            IEnumerable<Company> companies = await _repository.Company.GetAllAsync(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto?> GetByIdAsync(Guid id)
        {
            Company? company = await _repository.Company.GetByIdAsync(isTrackChanges: false, id);
            if (company == null)
            {
                _logger.LogWarning(BusinessLogs.ObjectNotExistInDB(nameof(Company), id));
                return null;
            }
            return _mapService.Execute<Company, CompanyDto>(company);
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await _repository.Company.IsValidIdAsync(id);
        }

        public async Task<bool> UpdateFullyAsync(Guid id, CompanyForUpdateDto updateDto)
        {
            bool result = true;
            Company? company = await _repository.Company.GetByIdAsync(isTrackChanges: true, id);
            if (company != null)
            {
                _mapService.Execute<CompanyForUpdateDto, Company>(updateDto, company);
                await _repository.SaveChangesAsync();
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
