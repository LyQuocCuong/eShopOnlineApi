using Shared.DTOs.Inputs.FromBody.UpdateDtos;
using Shared.DTOs.Outputs.EntityDtos;

namespace eShopOnlineFake.FakeEntities
{
    public static class FakeCompany
    {
        public static readonly Guid NonExistingCompanyId = new Guid("00000000-0000-0000-0000-000000000003");

        public static readonly CompanyDto CompanyNo1 = new CompanyDto()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000001"),
            Name = "Company 01",
            Address = "xxx",
            Phone = "xxx"
        };
        public static readonly CompanyDto CompanyNo2 = new CompanyDto()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000002"),
            Name = "Company 02",
            Address = "xxx",
            Phone = "xxx"
        };
        public static readonly IEnumerable<CompanyDto> ListOfCompanies = new List<CompanyDto>()
        {
            CompanyNo1,
            CompanyNo2
        };

        public static readonly CompanyForUpdateDto UpdateObject = new CompanyForUpdateDto()
        {
            Name = "Edited Company",
            Address = "xxx",
            Phone = "xxx"
        };
    }
}
