using AutoMapper;

namespace eShopOnlineUtilities.AutoMapper.Profiles
{
    public partial class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            MappingCompanyProfile();
            MappingCustomerProfile();
            MappingEmployeeProfile();
            MappingProductProfile();
            MappingStoreProfile();
        }
    }
}
