namespace FakeDataShared.FakeDataForEntities
{
    public sealed class FakeDataForCompany
    {
        public Guid GetExistingCompanyId()
        {
            return new Guid("00000000-0000-0000-0000-000000000001");
        }

        public Guid GetNonExistingCompanyId()
        {
            return new Guid("00000000-0000-0000-0000-000000000009");
        }

        public IEnumerable<CompanyDto> GetListOfCompanyDtos()
        {
            return new List<CompanyDto>()
            {
                new CompanyDto()
                {
                    Id = this.GetExistingCompanyId(),
                    Name = "Company 01"
                }
            };
        }

        public IEnumerable<Company> GetListOfCompanies()
        {
            return new List<Company>()
            {
                new Company()
                {
                    Id = this.GetExistingCompanyId(),
                    Name = "Company 01"
                }
            };
        }

        public CompanyForUpdateDto GetValidUpdateDto()
        {
            return new CompanyForUpdateDto()
            {
                Name = "Valid Object",
            };
        }

        public CompanyForUpdateDto GetInvalidUpdateDto()
        {
            return new CompanyForUpdateDto()
            {
                Name = "Invalid Object",
            };
        }

    }
}
