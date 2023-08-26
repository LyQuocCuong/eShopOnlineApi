namespace FakeDataShared.FakeDataForEntities
{
    public sealed class FakeDataForCompany
    {
        public static Guid GetNonExistingCompanyId()
        {
            return new Guid("00000000-0000-0000-0000-000000000001");
        }

        public static Guid GetNormalCompanyId()
        {
            return new Guid("10000000-0000-0000-0000-000000000001");
        }

        public CompanyDto GetNormalCompanyDto()
        {
            return new CompanyDto()
            {
                Id = GetNormalCompanyId(),
                Name = "A Normal Company",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public IEnumerable<CompanyDto> GetListOfCompanyDtos()
        {
            return new List<CompanyDto>()
            {
                GetNormalCompanyDto(),
            };
        }

        public Company GetNormalCompany()
        {
            return new Company()
            {
                Id = GetNormalCompanyId(),
                Name = "A Normal Company",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public IEnumerable<Company> GetListOfCompanies()
        {
            return new List<Company>()
            {
                GetNormalCompany(),
            };
        }

        public CompanyForUpdateDto GetValidUpdateDto()
        {
            return new CompanyForUpdateDto()
            {
                Name = "12345678",
                Address = "yyy",
                Phone = "12345678910"
            };
        }

        public CompanyForUpdateDto GetInvalidUpdateDto()
        {
            return new CompanyForUpdateDto()
            {
                Name = "Invalid",
                Address = "yyy",
                Phone = "yyy"
            };
        }

    }
}
