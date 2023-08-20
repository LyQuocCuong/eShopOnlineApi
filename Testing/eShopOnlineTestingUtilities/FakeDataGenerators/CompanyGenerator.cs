namespace eShopOnlineTestingUtilities.FakeDataGenerators
{
    public sealed class CompanyGenerator
    {
        public Guid GenerateNonExistingCompanyId()
        {
            return new Guid("00000000-0000-0000-0000-000000000003");
        }

        public CompanyDto GenerateCompanyNo1()
        {
            return new CompanyDto()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Company 01",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public CompanyDto GenerateCompanyNo2()
        {
            return new CompanyDto()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "Company 02",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public IEnumerable<CompanyDto> GenerateAListOfCompanies()
        {
            return new List<CompanyDto>() 
            {
                GenerateCompanyNo1(),
                GenerateCompanyNo2(),
            };
        }

        public CompanyForUpdateDto GenerateUpdateDataObj()
        {
            return new CompanyForUpdateDto()
            {
                Name = "Edited Company",
                Address = "xxx",
                Phone = "xxx"
            };
        }

    }
}
