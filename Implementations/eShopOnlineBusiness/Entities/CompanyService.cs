namespace eShopOnlineBusiness.Entities
{
    internal sealed class CompanyService : AbstractService, ICompanyService
    {
        protected override string ClassName => nameof(CompanyService);

        public CompanyService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            LogMethodInfo(nameof(GetAll));
            IEnumerable<Company> companies = _repository.Company.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }

        public CompanyDto? GetById(Guid id)
        {
            LogMethodInfo(nameof(GetById));
            Company? company = _repository.Company.GetById(isTrackChanges: false, id);
            if (company == null)
            {
                LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Company), id));
                return null;
            }
            return _mapService.Execute<Company, CompanyDto>(company);
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));
            return _repository.Company.IsValidId(id);
        }

        public bool UpdateFully(Guid id, CompanyForUpdateDto updateDto)
        {
            LogMethodInfo(nameof(UpdateFully));
            bool result = true;
            Company? company = _repository.Company.GetById(isTrackChanges: true, id);
            if (company != null)
            {
                _mapService.Execute<CompanyForUpdateDto, Company>(updateDto, company);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(BusinessLogMessages.ObjectNotExistInDB(nameof(Company), id));
                result = false;                
            }
            return result;
        }

    }
}
