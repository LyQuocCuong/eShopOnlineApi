namespace eShopOnlineBusiness.Entities
{
    internal sealed class CompanyService : AbstractService, ICompanyService
    {
        protected override string ChildClassName => nameof(CompanyService);

        public CompanyService(ServiceParams serviceParams) : base(serviceParams)
        {
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            IEnumerable<Company> companies = _repository.Company.GetAll(isTrackChanges: false);
            return _mapService.Execute<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }

        public CompanyDto? GetById(Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            Company? company = _repository.Company.GetById(isTrackChanges: false, id);
            if (company == null)
            {
                LogInfo(nameof(GetById), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Company), id.ToString()));
                return null;
            }
            return _mapService.Execute<Company, CompanyDto>(company);
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return _repository.Company.IsValidId(id);
        }

        public bool UpdateFully(Guid id, CompanyForUpdateDto updateDto)
        {
            bool result = true;
            LogInfo(nameof(UpdateFully), LogMessages.MessageForStartingMethodExecution);
            Company? company = _repository.Company.GetById(isTrackChanges: true, id);
            if (company != null)
            {
                _mapService.Execute<CompanyForUpdateDto, Company>(updateDto, company);
                _repository.SaveChanges();
            }
            else
            {
                LogInfo(nameof(UpdateFully), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Company), id.ToString()));
                result = false;                
            }
            LogInfo(nameof(UpdateFully), LogMessages.MessageForFinishingMethodExecution);
            return result;
        }

    }
}
